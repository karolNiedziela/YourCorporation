using MediatR;
using YourCorporation.Modules.JobSystem.MessagingContracts;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Handlers
{
    internal class WorkLocationCreatedHandler : INotificationHandler<WorkLocationCreated>
    {
        private readonly IWorkLocationRepository _workLocationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkLocationCreatedHandler(IWorkLocationRepository workLocationRepository, IUnitOfWork unitOfWork)
        {
            _workLocationRepository = workLocationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(WorkLocationCreated notification, CancellationToken cancellationToken)
        {
            var workLocation = new WorkLocation(new WorkLocationId(notification.Id), notification.Name);

            _workLocationRepository.Add(workLocation);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
