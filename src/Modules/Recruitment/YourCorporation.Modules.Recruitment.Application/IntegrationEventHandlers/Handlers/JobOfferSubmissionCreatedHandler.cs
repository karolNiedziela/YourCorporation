using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Forms.MessagingContracts;
using YourCorporation.Modules.Recruitment.Core.Candidates;
using YourCorporation.Modules.Recruitment.Core.Candidates.Repositories;
using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Handlers
{
    internal class JobOfferSubmissionCreatedHandler : INotificationHandler<JobOfferSubmissionCreated>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILogger<JobOfferSubmissionCreatedHandler> _logger;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public JobOfferSubmissionCreatedHandler(ICandidateRepository candidateRepository, ILogger<JobOfferSubmissionCreatedHandler> logger, IJobApplicationRepository jobApplicationRepository, IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _logger = logger;
            _jobApplicationRepository = jobApplicationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(JobOfferSubmissionCreated notification, CancellationToken cancellationToken)
        {
            var privateEmail = PrivateEmail.Create(notification.Email);
            if (privateEmail.IsFailure)
            {
                _logger.LogWarning($"Job offer submission won't be process due to invalid email address.");
                return;
            }

            var candidate = await _candidateRepository.GetAsync(privateEmail.Value);
            if (candidate is not null)
            {
                await CreateJobApplication(notification, candidate, cancellationToken);
                return;
            }

            var firstName = FirstName.Create(notification.FirstName);
            var lastName = LastName.Create(notification.LastName);
            if (firstName.IsFailure || lastName.IsFailure)
            {
                _logger.LogWarning("Invalid contact personal data, won't process job offer submission.");
                return;
            }

            candidate = Candidate.Create(firstName.Value, lastName.Value, privateEmail.Value);
            _candidateRepository.Add(candidate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("New candidate with '{CandidateId}' and private email '{PrivateEmail}'.", candidate.Id.Value, candidate.PrivateEmail.Value);           

            await CreateJobApplication(notification, candidate, cancellationToken);
        }

        private async Task CreateJobApplication(JobOfferSubmissionCreated notification, Candidate candidate, CancellationToken cancellationToken)
        {
            var chosenWorkLocationIds = notification.ChosenWorkLocationIds.Select(x => new WorkLocationId(x));
            var jobOffer = new JobOffer(notification.JobOfferId, notification.JobOfferName);
            var jobApplicationId = new JobApplicationId(Guid.NewGuid());
            var jobApplication = new JobApplication(
                notification.CVUrl,
                jobOffer,
                notification.JobOfferSubmissionId,
                candidate.Id,
                candidate.FirstName.Value,
                candidate.LastName.Value,
                chosenWorkLocationIds.Select(x => new JobApplicationChosenWorkLocation(jobApplicationId, x)),
                jobApplicationId);

            _jobApplicationRepository.Add(jobApplication);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("New job application with id '{JobApplicationId}'", jobApplication.Id.Value);
        }
    }
}
