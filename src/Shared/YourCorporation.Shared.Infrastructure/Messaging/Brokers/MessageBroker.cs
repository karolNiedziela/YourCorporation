using MassTransit;
using Microsoft.Extensions.Logging;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;

namespace YourCorporation.Shared.Infrastructure.Messaging.Brokers
{
    internal class MessageBroker : IMessageBroker
    {
        private readonly ILogger<MessageBroker> _logger;
        private readonly IMessageContextProvider _messageContextProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageBroker(ILogger<MessageBroker> logger, IMessageContextProvider messageContextProvider, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _messageContextProvider = messageContextProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(IMessage message, IMessage notification, CancellationToken cancellationToken = default)
        {
            var messageContext = _messageContextProvider.Get(notification);

            var module = message.GetModuleName();
            var name = message.GetType().Name;
            var requestId = messageContext.Context.RequestId;
            var traceId = messageContext.Context.TraceId;
            var correlationId = messageContext.Context.CorrelationId;
            var messageId = messageContext.MessageId;

            _logger.LogInformation("Publishing a message: {Name} ({Module}) [Request Id: {RequestId}, Message Id: {MessageId}, Correlation Id: {CorrelationId}, Trace Id: '{TraceId}']...",
                name, module, requestId, messageId, correlationId, traceId);

            await _publishEndpoint.Publish(message, publishContext =>
            {
                publishContext.CorrelationId = correlationId;
                publishContext.MessageId = messageId;
                publishContext.RequestId = requestId;
            }, cancellationToken);
        }        
    }
}
