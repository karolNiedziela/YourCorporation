using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Modules.JobSystem.MessagingContracts;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Handlers
{
    internal class WorkLocationCreatedHandler : INotificationHandler<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;
        private readonly ILogger<WorkLocationCreatedHandler> _logger;

        public WorkLocationCreatedHandler(IWorkLocationRepository workLocationRepository, ILogger<WorkLocationCreatedHandler> logger)
        {
            _workLocationRepository = workLocationRepository;
            _logger = logger;
        }

        public async Task Handle(WorkLocationCreated notification, CancellationToken cancellationToken)
        {
            var workLocation = new WorkLocation(notification.Id, notification.Name, notification.Code);

            await _workLocationRepository.AddAsync(workLocation);
        }
    }
}
