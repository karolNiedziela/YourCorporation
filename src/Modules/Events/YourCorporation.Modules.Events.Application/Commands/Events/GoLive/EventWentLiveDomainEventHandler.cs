using MassTransit;
using MediatR;
using YourCorporation.Modules.Events.Core.Events.Events;
using YourCorporation.Modules.Events.MessagingContracts;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal sealed class EventWentLiveDomainEventHandler : INotificationHandler<EventWentLiveDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventWentLiveDomainEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(EventWentLiveDomainEvent notification, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new EventWentLive(
                notification.Event.Id.Value,
                notification.Event.Name.Value,
                notification.Event.BegginingAndEndOfEvent.StartTime,
                notification.Event.BegginingAndEndOfEvent.EndTime
                ), cancellationToken);
        }
    }
}
