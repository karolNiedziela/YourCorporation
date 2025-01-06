using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core;
using YourCorporation.Modules.Recruitment.Core.Contacts.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;

namespace YourCorporation.Modules.Recruitment.Application.Features.Contacts.ContactStatusUpdatedFromJobApplication
{
    internal class ContactStatusUpdatedFromJobApplicationEventHandler : INotificationHandler<ContactStatusUpdatedFromJobApplicationDomainEvent>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly ILogger<ContactStatusUpdatedFromJobApplicationDomainEvent> _logger;
        private readonly TimeProvider _timeProvider;

        public ContactStatusUpdatedFromJobApplicationEventHandler(IJobApplicationRepository jobApplicationRepository, ILogger<ContactStatusUpdatedFromJobApplicationDomainEvent> logger, TimeProvider timeProvider)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _logger = logger;
            _timeProvider = timeProvider;
        }

        public async Task Handle(ContactStatusUpdatedFromJobApplicationDomainEvent notification, CancellationToken cancellationToken)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(notification.JobApplicationId);
            if (jobApplication is null)
            {                
                _logger.LogInformation(ErrorCodes.JobApplications.NotFoundError(notification.JobApplicationId).Message);
                return;
            }

            jobApplication.Complete(_timeProvider.GetUtcNow());

            _jobApplicationRepository.Update(jobApplication);
        }
    }
}
