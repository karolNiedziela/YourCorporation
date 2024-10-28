using MediatR;
using YourCorporation.Modules.Recruitment.Core;
using YourCorporation.Modules.Recruitment.Core.Queues;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.Repositories;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Application.Features.RecruitmentQueues.AddRecruitmentQueue
{
    internal class AddRecruitmentQueueCommandHandler : IRequestHandler<AddRecruitmentQueueCommand, Result<Guid>>
    {
        private readonly IRecruitmentQueueRepository _recruitmentQueueRepository;
        private readonly IWorkLocationRepository _workLocationRepository;

        public AddRecruitmentQueueCommandHandler(IRecruitmentQueueRepository recruitmentQueueRepository, IWorkLocationRepository workLocationRepository)
        {
            _recruitmentQueueRepository = recruitmentQueueRepository;
            _workLocationRepository = workLocationRepository;
        }

        public async Task<Result<Guid>> Handle(AddRecruitmentQueueCommand request, CancellationToken cancellationToken)
        {
            var existingRecruitmentQueue = await _recruitmentQueueRepository.GetAsync(request.Name);
            if (existingRecruitmentQueue is not null)
            {
                return ErrorCodes.RecruitmentQueues.AlreadyExistsError(request.Name);
            }

            var nonExistingLocations = await _workLocationRepository.GetNonExistignWorkLocationsAsync(request.WorkLocationIds);
            if (nonExistingLocations.Any())
            {
                return ErrorCodes.RecruitmentQueues.NotExistingWorkLocationError;
            }

            var recruitmentQueueNameResult = RecruitmentQueueName.Create(request.Name);
            if (recruitmentQueueNameResult.IsFailure)
            {
                return recruitmentQueueNameResult.Errors;
            }

            var recruitmentQueue = RecruitmentQueue.Create(recruitmentQueueNameResult.Value, request.WorkLocationIds.Select(x => new WorkLocationId(x)));

            _recruitmentQueueRepository.Add(recruitmentQueue);

            return recruitmentQueue.Id.Value;
        }
    }
}
