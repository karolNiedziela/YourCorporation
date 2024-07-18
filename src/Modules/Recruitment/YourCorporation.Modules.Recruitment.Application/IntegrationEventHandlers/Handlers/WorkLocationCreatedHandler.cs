using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Handlers
{
    internal class WorkLocationCreatedHandler : INotificationHandler<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;
        private readonly ILogger<WorkLocationCreatedHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public WorkLocationCreatedHandler(IWorkLocationRepository workLocationRepository, ILogger<WorkLocationCreatedHandler> logger, IUnitOfWork unitOfWork)
        {
            _workLocationRepository = workLocationRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(WorkLocationCreated notification, CancellationToken cancellationToken)
        {
            var existingWorkLocation = await _workLocationRepository.GetAsync(notification.Id);
            if (existingWorkLocation is not null)
            {
                _logger.LogInformation($"Work location with id '{notification.Id}' already exists.");
                return;
            }

            var workLocation = new WorkLocation(new WorkLocationId(notification.Id), notification.Name);

            _workLocationRepository.Add(workLocation);

            await _unitOfWork.SaveChangesAsync(workLocation, cancellationToken);
        }
    }
}
