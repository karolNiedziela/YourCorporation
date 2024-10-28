using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Forms.MessagingContracts;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Application.IntegrationEventHandlers.Handlers
{
    internal class JobOfferSubmissionCreatedHandler : INotificationHandler<JobOfferSubmissionCreated>
    {
        private readonly ILogger<JobOfferSubmissionCreatedHandler> _logger;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobOfferSubmissionCreatedHandler(ILogger<JobOfferSubmissionCreatedHandler> logger, IJobApplicationRepository jobApplicationRepository)
        {
            _logger = logger;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public Task Handle(JobOfferSubmissionCreated notification, CancellationToken cancellationToken)
        {
            var privateEmail = PrivateEmail.Create(notification.Email);
            if (privateEmail.IsFailure)
            {
                _logger.LogWarning($"Job offer submission won't be process due to invalid email address.");
                return Task.CompletedTask;
            }

            var firstName = FirstName.Create(notification.FirstName);
            var lastName = LastName.Create(notification.LastName);
            if (firstName.IsFailure || lastName.IsFailure)
            {
                _logger.LogWarning("Invalid application personal data, won't process job offer submission.");
                return Task.CompletedTask;
            }

            var chosenWorkLocationIds = notification.ChosenWorkLocationIds.Select(x => new WorkLocationId(x));
            var jobApplicationId = new JobApplicationId(Guid.NewGuid());
            var jobApplication = new JobApplication(
                notification.CVUrl,
                notification.JobOfferId,
                notification.JobOfferSubmissionId,
                firstName.Value,
                lastName.Value,
                privateEmail.Value,
                chosenWorkLocationIds,
                jobApplicationId);

            _jobApplicationRepository.Add(jobApplication);

            _logger.LogInformation("New job application with id '{JobApplicationId}'", jobApplication.Id.Value);

            return Task.CompletedTask;
        }
    }
}
