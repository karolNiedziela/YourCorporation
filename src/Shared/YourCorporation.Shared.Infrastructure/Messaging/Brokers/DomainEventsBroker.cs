﻿using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using YourCorporation.Shared.Abstractions.Contexts;
using YourCorporation.Shared.Abstractions.Extensions;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;
using YourCorporation.Shared.Abstractions.Messaging.Outbox;
using YourCorporation.Shared.Infrastructure.Messaging.Outbox;

namespace YourCorporation.Shared.Infrastructure.Messaging.Brokers
{
    internal class DomainEventsBroker : IDomainEventsBroker
    {
        private readonly ILogger<DomainEventsBroker> _logger;
        private readonly IMessageContextRegistry _messageContextRegistry;
        private readonly IMessageContextProvider _messageContextProvider;
        private readonly IContext _context;
        private readonly IOutboxBroker _outboxBroker;

        public bool Enabled { get; }

        public DomainEventsBroker(
            IOptions<OutboxOptions> outboxOptions,
            ILogger<DomainEventsBroker> logger,
            IMessageContextRegistry messageContextRegistry,
            IContext context,
            IOutboxBroker outboxBroker,
            IMessageContextProvider messageContextProvider)
        {
            Enabled = outboxOptions.Value.Enabled;
            _logger = logger;
            _messageContextRegistry = messageContextRegistry;
            _context = context;
            Enabled = outboxOptions.Value.Enabled;
            _outboxBroker = outboxBroker;
            _messageContextProvider = messageContextProvider;
        }

        public async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
            => await PublishAsync(cancellationToken, message);

        public async Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default)
            => await PublishAsync(cancellationToken, messages);

        public async Task PublisFromNotificationHandlerAsync(IMessage sourceNotification, IMessage message, CancellationToken cancellationToken = default)
            => await PublisFromNotificationHandlerAsync(sourceNotification, cancellationToken, message);

        public async Task PublisFromNotificationHandlerAsync(IMessage sourceNotification, IMessage[] messages, CancellationToken cancellationToken = default)
            => await PublisFromNotificationHandlerAsync(sourceNotification, cancellationToken, messages);

        private async Task PublishAsync(CancellationToken cancellationToken, params IMessage[] messages)
        {
            if (messages.Length == 0)
            {
                return;
            }

            if (!Enabled)
            {
                _logger.LogWarning("Outbox is disabled");
                return;
            }

            foreach (var message in messages)
            {
                var messageContext = new Contexts.MessageContext(Guid.NewGuid(), _context);

                _messageContextRegistry.Set(message, messageContext);

                var module = message.GetModuleName();
                var name = message.GetType().Name;
                var requestId = _context.RequestId;
                var traceId = _context.TraceId;


                _logger.LogInformation("Domain event: {Name} ({ModuleName}) [Request Id: {RequestId}, Message Id: {MessageId}, Correlation Id: {CorrelationId}, Trace Id: '{TraceId}']...",
                    name, module, requestId, messageContext.MessageId, messageContext.Context.CorrelationId, traceId);
            }

            await _outboxBroker.SendAsync(messages);
        }

        private async Task PublisFromNotificationHandlerAsync(IMessage sourceNotification, CancellationToken cancellationToken, params IMessage[] messages)
        {
            if (messages.Length == 0)
            {
                return;
            }

            if (!Enabled)
            {
                _logger.LogWarning("Outbox is disabled");
                return;
            }

            var messageContext = _messageContextProvider.Get(sourceNotification);

            foreach (var message in messages)
            {               
                messageContext ??= new Contexts.MessageContext(Guid.NewGuid(), _context);

                _messageContextRegistry.Set(message, messageContext);

                var module = message.GetModuleName();
                var name = message.GetType().Name;
                var requestId = _context?.RequestId;
                var traceId = _context?.TraceId;


                _logger.LogInformation("Domain event: {Name} ({ModuleName}) [Request Id: {RequestId}, Message Id: {MessageId}, Correlation Id: {CorrelationId}, Trace Id: '{TraceId}']...",
                    name, module, requestId, messageContext.MessageId, messageContext.Context.CorrelationId, traceId);
            }

            await _outboxBroker.SendAsync(messages);
        }
    }
}
