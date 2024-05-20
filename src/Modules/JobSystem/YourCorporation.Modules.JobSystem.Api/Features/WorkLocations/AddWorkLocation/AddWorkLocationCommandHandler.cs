using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Events;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Repositories;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.WorkLocations.AddWorkLocation
{
    internal class AddWorkLocationCommandHandler : IRequestHandler<AddWorkLocationCommand, Result<Guid>>
    {
        private readonly IWorkLocationRepository _workLocationRepository;
        private readonly IPublisher _publisher;

        public AddWorkLocationCommandHandler(IWorkLocationRepository workLocationRepository, IPublisher publisher)
        {
            _workLocationRepository = workLocationRepository;
            _publisher = publisher;
        }

        public async Task<Result<Guid>> Handle(AddWorkLocationCommand request, CancellationToken cancellationToken)
        {
            var existingWorkLocation = request.Id.HasValue ?
                await _workLocationRepository.GetAsync(request.Id.Value, request.Code) :
                await _workLocationRepository.GetAsync(request.Code);
            if (existingWorkLocation is not null)
            {
                return Error.Conflict("WorkLocation.AlreadyExists", "Work location with given parameters already exists.");
            }

            var workLocationCodeResult = WorkLocationCode.Create(request.Code);
            if (workLocationCodeResult.IsFailure)
            {
                return workLocationCodeResult.Errors;
            }
            var workLocation = new WorkLocation(request.Name, workLocationCodeResult.Value, request.Id);

            await _workLocationRepository.AddAsync(workLocation);

            await _publisher.Publish(new WorkLocationCreatedDomainEvent(Guid.NewGuid(), workLocation), cancellationToken);

            return workLocation.Id;
        }
    }
}
