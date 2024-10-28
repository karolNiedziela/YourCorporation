using MediatR;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Events;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Repositories;
using YourCorporation.Shared.Abstractions.Messaging.Brokers;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Forms.Api.Features.FormSubmissions.JobOfferSubmissions.SubmitJobOffer
{
    internal class SubmitJobOfferCommandHandler : IRequestHandler<SubmitJobOfferCommand, Result<Guid>>
    {
        private readonly IJobOfferSubmissionRepository _jobOfferSubmissionRepository;
        private readonly IWorkLocationRepository _workLocationRepository;
        private readonly IJobOfferFormRepository _jobOfferFormRepository;
        private readonly IDomainEventsBroker _domainEventsBroker;

        public SubmitJobOfferCommandHandler(
            IJobOfferSubmissionRepository jobOfferSubmissionRepository,
            IWorkLocationRepository workLocationRepository,
            IJobOfferFormRepository jobOfferFormRepository,
            IDomainEventsBroker domainEventsBroker)
        {
            _jobOfferSubmissionRepository = jobOfferSubmissionRepository;
            _workLocationRepository = workLocationRepository;
            _jobOfferFormRepository = jobOfferFormRepository;
            _domainEventsBroker = domainEventsBroker;
        }

        public async Task<Result<Guid>> Handle(SubmitJobOfferCommand request, CancellationToken cancellationToken)
        {
            var jobOffer = await _jobOfferFormRepository.GetAsync(request.JobOfferFormId);
            if (jobOffer is null)
            {
                return Error.NotFound("JobOfferSubmission.JobOfferFormNotFound", $"Job offer form with id '{request.JobOfferFormId}' was not found.");
            }

            foreach (var chosenWorkLocationId in request.ChosenWorkLocations)
            {
                if (await _workLocationRepository.GetAsync(chosenWorkLocationId) is null)
                {
                    return Error.NotFound("JobOfferSubmission.WorkLocationNotFound", $"Work location with id '{chosenWorkLocationId}' was not found.");
                }
            }

            var jobOfferSubmissionId = Guid.NewGuid();
            var chosenWorkLocations = request.ChosenWorkLocations
                .Select(x => new JobOfferSubmissionChosenWorkLocation
                {
                    JobOfferSubmissionId = jobOfferSubmissionId,
                    WorkLocationId = x,
                }).ToList();

            // TODO: Save CV to storage;
            var cvUrl = request.Cv.FileName;

            var jobOfferSubmission = new JobOfferSubmission(
               jobOfferSubmissionId,
                jobOffer.Id,
                request.FirstName,
                request.LastName,
                request.Email,
                cvUrl,
                chosenWorkLocations);          

            await _jobOfferSubmissionRepository.AddAsync(jobOfferSubmission);

            await _domainEventsBroker.PublishAsync(
                new JobOfferSubmissionCreatedDomainEvent(
                JobOfferSubmissionId: jobOfferSubmission.Id,
                FirstName: jobOfferSubmission.FirstName,
                LastName: jobOfferSubmission.LastName,
                CvUrl: jobOfferSubmission.CVUrl,
                Email: jobOfferSubmission.Email,
                WorkLocationIds: jobOfferSubmission.ChosenWorkLocations.Select(x => x.Id).ToList(),
                JobOfferId: jobOfferSubmission.JobOfferFormId), cancellationToken);

            return jobOfferSubmission.Id;
        }
    }
}
