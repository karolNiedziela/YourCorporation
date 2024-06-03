using MediatR;
using YourCorporation.Modules.Events.Core.Events.Events;
using YourCorporation.Modules.Events.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal sealed class EventWentLiveDomainEventHandler : INotificationHandler<EventWentLiveDomainEvent>
    {
        private readonly IMessageBroker _messageBroker;

        public EventWentLiveDomainEventHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task Handle(EventWentLiveDomainEvent notification, CancellationToken cancellationToken)
        {
            await _messageBroker.PublishAsync(new EventWentLive(
                notification.EventId,
                notification.EventName,
                notification.BegginningOfEvent,
                notification.EndOfEvent
                ), notification, cancellationToken);
        }
    }
}
