﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;
using YourCorporation.Shared.Infrastructure.Contexts;

namespace YourCorporation.Shared.Infrastructure.Messaging.Outbox
{
    internal sealed class Outbox<T> : IOutbox where T : DbContext
    {
        private readonly T _dbContext;
        private readonly DbSet<OutboxMessage> _outboxMessages;
        private readonly ILogger<Outbox<T>> _logger;
        private readonly TimeProvider _timeProvider;
        private readonly IPublisher _publisher;
        private readonly IMessageContextRegistry _messageContextRegistry;
        private readonly IMessageContextProvider _messageContextProvider;

        public Outbox(
            T dbContext,
            ILogger<Outbox<T>> logger,
            IOptions<OutboxOptions> outboxOptions,
            TimeProvider timeProvider,
            IPublisher publisher,
            IMessageContextRegistry messageContextRegistry,
            IMessageContextProvider messageContextProvider)
        {
            _dbContext = dbContext;
            _outboxMessages = dbContext.Set<OutboxMessage>();
            _logger = logger;
            Enabled = outboxOptions.Value.Enabled;
            _timeProvider = timeProvider;
            _publisher = publisher;
            _messageContextRegistry = messageContextRegistry;
            _messageContextProvider = messageContextProvider;
        }

        public bool Enabled { get; set; }       

        public async Task SaveAsync(params IMessage[] messages)
        {
            var moduleName = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning($"Outbox is disabled ('{moduleName}'), outgoing messages won't be saved.");
                return;
            }

            if (messages is null || messages.Length == 0)
            {
                _logger.LogWarning($"No messages have beed provided to be saved to the outbox ('{moduleName}').");
                return;
            }

            var outboxMessages = messages
                .Select(message =>
                {
                    var context = _messageContextProvider.Get(message);

                    return new OutboxMessage
                    {

                        Id = Guid.NewGuid(),
                        Name = message.GetType().Name,
                        CorrelationId = context.Context.CorrelationId,
                        TraceId = context.Context.TraceId,
                        Type = message.GetType().AssemblyQualifiedName!,
                        CreatedAt = _timeProvider.GetUtcNow().DateTime,
                        Content = JsonConvert.SerializeObject(message, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                    };        
                }).ToArray();

            if (outboxMessages.Length == 0)
            {
                _logger.LogWarning($"No messages have beed provided to be saved to the outbox ('{moduleName}').");
                return;
            }

            await _outboxMessages.AddRangeAsync(outboxMessages);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Saved {outboxMessages.Length} messages to the outbox ('{moduleName}').");
        }

        public async Task PublishUnsentAsync()
        {
            var moduleName = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning($"Outbox is disabled ('{moduleName}'), outgoing messages won't be saved.");
                return;
            }

            var unsentMessages = await _outboxMessages.Where(x => x.SentAt == null).ToListAsync();
            if (!unsentMessages.Any())
            {
                _logger.LogTrace($"No unsent messages found in outbox ('{moduleName}').");
                return;
            }

            _logger.LogTrace($"Found {unsentMessages.Count} unsent messages in outbox ('{moduleName}'), publishing...");

            foreach (var outboxMessage in unsentMessages )
            {
                var type = Type.GetType(outboxMessage.Type);

                var message = JsonConvert.DeserializeObject<IMessage>(outboxMessage.Content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                if (message is null)
                {
                    _logger.LogError($"Invalid message type in outbox ('{moduleName}'): '{type!.Name}', name: '{outboxMessage.Name}', " +
                               $"Id: '{outboxMessage.Id}' ('{moduleName}').");
                    continue;
                }

                var messageId = outboxMessage.Id;
                var correlationId = outboxMessage.CorrelationId;
                var sentAt = _timeProvider.GetUtcNow().DateTime;
                var name = message.GetType().Name;
                var traceId = outboxMessage.TraceId;

                var context = new Context(correlationId, outboxMessage.TraceId);
                var messageContext = new Contexts.MessageContext(messageId, context);
                _messageContextRegistry.Set(message, messageContext);

                _logger.LogInformation("Publishing a message from outbox from ('{ModuleName}'): {Name} [Message Id: {MessageId}, Correlation Id: {CorrelationId}, Trace Id: '{TraceId}']...",
                    moduleName, name, messageId, correlationId, traceId);

                await _publisher.Publish(message);

                outboxMessage.SentAt = sentAt;
                _outboxMessages.Update(outboxMessage);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task CleanupAsync(DateTime? to = null)
        {
            var moduleName = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning($"Outbox is disabled ('{moduleName}'), outgoing messages won't be saved.");
                return;
            }

            var cleanupToDate = to ?? _timeProvider.GetUtcNow().DateTime;
            var sentMessages = await _outboxMessages.Where(x => x.SentAt != null && x.CreatedAt <= cleanupToDate).ToListAsync();
            if (!sentMessages.Any())
            {
                _logger.LogTrace($"No sent messages found in outbox ('{moduleName}') till: {cleanupToDate}.");
                return;
            }

            _logger.LogTrace($"Found {sentMessages.Count} sent messages in outbox ('{moduleName}') till: {cleanupToDate}, cleaning up...");

            _outboxMessages.RemoveRange(sentMessages);
            await _dbContext.SaveChangesAsync();
            
            _logger.LogTrace($"Removed {sentMessages.Count} sent messages from outbox ('{moduleName}') till: {cleanupToDate}.");
        }
    }
}
