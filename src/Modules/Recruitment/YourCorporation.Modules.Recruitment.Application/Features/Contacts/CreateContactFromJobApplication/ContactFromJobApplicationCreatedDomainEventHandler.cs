using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.Contacts.Events;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;

namespace YourCorporation.Modules.Recruitment.Application.Features.Contacts.CreateContactFromJobApplication
{
    internal class ContactFromJobApplicationCreatedDomainEventHandler : INotificationHandler<ContactFromJobApplicationCreatedDomainEvent>
    {
        private readonly ILogger<ContactFromJobApplicationCreatedDomainEventHandler> _logger;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public ContactFromJobApplicationCreatedDomainEventHandler(ILogger<ContactFromJobApplicationCreatedDomainEventHandler> logger, IJobApplicationRepository jobApplicationRepository)
        {
            _logger = logger;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task Handle(ContactFromJobApplicationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(notification.JobApplicationId);
            jobApplication.AssignContact(new ContactId(notification.ContactId));
            _jobApplicationRepository.Update(jobApplication);

            _logger.LogInformation($"Contact with id '{notification.ContactId}' assigned to Job Application with id '{notification.JobApplicationId}'.");
        }
    }
}
