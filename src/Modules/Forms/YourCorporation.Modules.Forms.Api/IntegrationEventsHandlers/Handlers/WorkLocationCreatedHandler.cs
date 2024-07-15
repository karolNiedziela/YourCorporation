using MediatR;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Handlers
{
    internal class WorkLocationCreatedHandler : INotificationHandler<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;

        public WorkLocationCreatedHandler(IWorkLocationRepository workLocationRepository)
        {
            _workLocationRepository = workLocationRepository;
        }

        public async Task Handle(WorkLocationCreated notification, CancellationToken cancellationToken)
        {
            var workLocation = new WorkLocation(notification.Id, notification.Name, notification.Code);

            await _workLocationRepository.AddAsync(workLocation);
        }
    }
}
