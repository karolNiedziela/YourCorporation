using MediatR;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Handlers
{
    internal class WorkLocationCreatedHandler : INotificationHandler<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;

        public WorkLocationCreatedHandler(IWorkLocationRepository workLocationRepository)
        {
            _workLocationRepository = workLocationRepository;
        }

        public Task Handle(WorkLocationCreated notification, CancellationToken cancellationToken)
        {
            var workLocation = new WorkLocation(new WorkLocationId(notification.Id), notification.Name);

            _workLocationRepository.Add(workLocation);

            return Task.CompletedTask;
        }
    }
}
