using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Handlers
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
            var workLocation = new WorkLocation(new WorkLocationId(notification.Id), notification.Name, notification.Code);

            _workLocationRepository.Add(workLocation);
        }
    }
}
