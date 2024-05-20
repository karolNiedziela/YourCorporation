using MediatR;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers;
using YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Repositiories;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;
using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Repositories;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers.CreateJobOffer
{
    internal class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, Result<Guid>>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IWorkLocationRepository _workLocationRepository;

        public CreateJobOfferCommandHandler(IJobOfferRepository jobOfferRepository, IWorkLocationRepository workLocationRepository)
        {
            _jobOfferRepository = jobOfferRepository;
            _workLocationRepository = workLocationRepository;
        }

        public async Task<Result<Guid>> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var existingWorkLocations = new List<WorkLocation>();
            foreach (var workLocationId in request.WorkLocations)
            {
                var workLocation = await _workLocationRepository.GetAsync(workLocationId);
                if (workLocation is null)
                {
                    return Error.NotFound("JobOffers.WorkLocationNotFound", $"Work Location with id '{workLocationId}' was not found.");
                }

                existingWorkLocations.Add(workLocation);
            }

            var jobOffer = new JobOffer(request.Name, existingWorkLocations);

            await _jobOfferRepository.AddAsync(jobOffer);

            return jobOffer.Id;
        }
    }
}
