using MassTransit;
using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Events;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.JobSystem.Api.Features.WorkLocations.AddWorkLocation
{
    internal class WorkLocationCreatedDomainEventHandler : INotificationHandler<WorkLocationCreatedDomainEvent>
    {
        private readonly IPublishEndpoint _endpoint;

        public WorkLocationCreatedDomainEventHandler(IPublishEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task Handle(WorkLocationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _endpoint.Publish(new WorkLocationCreated(notification.WorkLocation.Id, notification.WorkLocation.Name), cancellationToken);
        }
    }
}
