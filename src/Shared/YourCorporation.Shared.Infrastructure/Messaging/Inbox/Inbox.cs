using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using YourCorporation.Shared.Abstractions.Extensions;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;
using YourCorporation.Shared.Abstractions.Messaging.Inbox;
using YourCorporation.Shared.Infrastructure.Contexts;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Inbox
{
    internal class Inbox<T> : IInbox where T : DbContext
    {
        private readonly T _dbContext;
        private readonly DbSet<InboxMessage> _inboxMessages;
        private readonly TimeProvider _timeProvider;
        private readonly ILogger<Inbox<T>> _logger;
        private readonly IMessageContextRegistry _messageContextRegistry;
        private readonly IInboxNotificationPublisher _publisher;

        public bool Enabled { get; }

        public Inbox(
            T dbContext,
            TimeProvider timeProvider,
            ILogger<Inbox<T>> logger,
            IOptions<OutboxOptions> options,
            IMessageContextRegistry messageContextRegistry,
            IInboxNotificationPublisher publisher)
        {
            _dbContext = dbContext;
            _inboxMessages = _dbContext.Set<InboxMessage>();
            _timeProvider = timeProvider;
            _logger = logger;
            Enabled = options.Value.Enabled;
            _messageContextRegistry = messageContextRegistry;
            _publisher = publisher;
        }

        public async Task SaveAsync(ConsumeContext<IMessage> consumeContext)
        {
            var moduleName = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning($"Inbox is disabled ('{moduleName}'), incoming messages won't be processed.");
                return;
            }

            var isAlreadyProcessed = await _inboxMessages.AnyAsync(x => x.Id == consumeContext.MessageId && x.ProcessedAt != null);
            if (isAlreadyProcessed)
            {
                _logger.LogTrace("Message with Id: '{MessageId}' was already processed ({ModuleName}).", consumeContext.MessageId, moduleName);
                return;
            }

            _logger.LogTrace("Received a message [Message Id: '{MessageId}', CorrelationId: '{CorrelationId}']" +
                " to be processed ('{ModuleName}').", consumeContext.MessageId, consumeContext.CorrelationId, moduleName);

            var inboxMessage = new InboxMessage
            {
                Id = Guid.NewGuid(),
                CorrelationId = consumeContext.CorrelationId.Value,
                Name = consumeContext.Message.GetType().Name,
                Type = consumeContext.Message.GetType().AssemblyQualifiedName!,
                ReceivedAt = _timeProvider.GetUtcNow().DateTime,
                Content = JsonConvert.SerializeObject(consumeContext.Message, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            };

            await _inboxMessages.AddAsync(inboxMessage);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Saved a message [Message Id: '{MessageId}', CorrelationId: '{CorrelationId}']" +
                " to be processed ('{ModuleName}').", consumeContext.MessageId, consumeContext.CorrelationId, moduleName);
        }

        public async Task ProcessUnprocessedAsync()
        {
            var moduleName = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning("Inbox is disabled ('{ModuleName}'), incoming messages won't be processed.", moduleName);
                return;
            }

            var unprocessedMessages = await _inboxMessages.Where(x => x.ProcessedAt == null).ToListAsync();
            if (!unprocessedMessages.Any()) 
            {
                _logger.LogTrace("No unprocessed messages found in inbox ('{ModuleName}').", moduleName);
                return;
            }

            _logger.LogTrace("Found {UnprocessedMessagesCount} unprocessed messages in inbox ('{ModuleName}'), processing...", unprocessedMessages.Count, moduleName);

            foreach (var inboxMessage in unprocessedMessages)
            {
                var type = Type.GetType(inboxMessage.Type);

                var message = JsonConvert.DeserializeObject<IMessage>(inboxMessage.Content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                if (message is null)
                {
                    _logger.LogError("Invalid message type in inbox ('{ModuleName}'): '{MessageType}', name: '{InboxMessageName}', " +
                             "Id: '{MessageId}' ('{ModuleName}').", moduleName, type!.Name, inboxMessage.Name, inboxMessage.Id, moduleName);
                    continue;
                }

                var messageId = inboxMessage.Id;
                var correlationId = inboxMessage.CorrelationId;
                var messageTypeName = message.GetType().Name;

                var context = new Context(correlationId);
                var messageContext = new Contexts.MessageContext(messageId, context);
                _messageContextRegistry.Set(message, messageContext);

                _logger.LogInformation("Start processing a message from inbox ('{ModuleName}'): {Name} [Message Id: {MessageId}, Correlation Id: {CorrelationId}]...",
                    moduleName, messageTypeName, messageId, correlationId);

                try
                {
                    await _publisher.Publish(message, moduleName);
                }
                catch (Exception exception)
                {
                    _logger.LogError("Error {InboxExceptionMessage} when proccessing inbox message ('{ModuleName}'): {Name} [Message Id: {MessageId}, Correlation Id: {CorrelationId}]).",
                        exception.Message, moduleName, messageTypeName, messageId, correlationId);
                    throw;
                }

                inboxMessage.ProcessedAt = _timeProvider.GetUtcNow().DateTime;

                _logger.LogInformation("End processing a message from inbox ('{ModuleName}'): {MessageType} [Message Id: {MessageId}, Correlation Id: {CorrelationId}]...",
                    moduleName, messageTypeName, messageId, correlationId);

                _inboxMessages.Update(inboxMessage);
                await _dbContext.SaveChangesAsync();
            }
        }       

        public async Task CleanupAsync(DateTime? to = null)
        {
            var moduleName = _dbContext.GetModuleName();
            if (!Enabled)
            {
                _logger.LogWarning("Inbox is disabled ('{ModuleName}'), incoming messages won't be processed.", moduleName);
                return;
            }

            var dateTo = to ?? _timeProvider.GetUtcNow().DateTime;
            var processedMessages = await _inboxMessages.Where(x => x.ReceivedAt <= dateTo && x.ProcessedAt != null).ToListAsync();
            if (!processedMessages.Any())
            {
                _logger.LogTrace("No processed messages found in inbox ('{ModuleName}') till: {TillInboxDateTo}.", moduleName, dateTo);
                return;
            }

            _logger.LogInformation("Found {ProcessedMessagesCount} processed messages in inbox ('{ModuleName}') till: {TillInboxDateTo}, cleaning up...", processedMessages.Count, moduleName, dateTo);
            _inboxMessages.RemoveRange(processedMessages);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Removed {ProcessedMessagesCountt} processed messages from inbox ('{ModuleName}') till: {TillInboxDateTo}.", processedMessages.Count, moduleName, dateTo);
        }
    }
}
