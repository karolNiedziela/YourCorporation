using MassTransit;
using MediatR;
using YourCorporation.Modules.Events.Core.Events.Events;
using YourCorporation.Modules.Events.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal sealed class EventWentLiveDomainEventHandler : INotificationHandler<EventWentLiveDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageContextProvider _messageContextProvider;

        public EventWentLiveDomainEventHandler(IPublishEndpoint publishEndpoint, IMessageContextProvider messageContextProvider)
        {
            _publishEndpoint = publishEndpoint;
            _messageContextProvider = messageContextProvider;
        }

        public async Task Handle(EventWentLiveDomainEvent notification, CancellationToken cancellationToken)
        {
            var messageContext = _messageContextProvider.Get(notification);

            await _publishEndpoint.Publish(new EventWentLive(
                notification.EventId,
                notification.EventName,
                notification.BegginningOfEvent,
                notification.EndOfEvent
                ), cancellationToken);
        }
    }
}
