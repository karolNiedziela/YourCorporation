using MassTransit;
using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Events;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;

namespace YourCorporation.Modules.JobSystem.Api.Features.WorkLocations.AddWorkLocation
{
    internal class WorkLocationCreatedDomainEventHandler : INotificationHandler<WorkLocationCreatedDomainEvent>
    {
        private readonly IMessageBroker _messageBroker;

        public WorkLocationCreatedDomainEventHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task Handle(WorkLocationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _messageBroker.PublishAsync(
                new WorkLocationCreated(notification.Id, notification.Name, notification.Code), 
                notification, 
                cancellationToken);
        }
    }
}
