using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.Contacts.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;

namespace YourCorporation.Modules.Recruitment.Application.Features.JobApplications.CreateJobApplication
{
    internal class JobApplicationCreatedDomainEventHandler : INotificationHandler<JobApplicationCreatedDomainEvent>
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<JobApplicationCreatedDomainEventHandler> _logger;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationCreatedDomainEventHandler(IContactRepository contactRepository, ILogger<JobApplicationCreatedDomainEventHandler> logger, IJobApplicationRepository jobApplicationRepository)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task Handle(JobApplicationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var existingContact = await _contactRepository.GetAsync(notification.ApplicationEmail);
            if (existingContact is not null)
            {
                await AssignContactToJobApplication(existingContact, notification.JobApplicationId, cancellationToken);
                return;
            }

            var contact = Contact.Create(notification.ApplicationFirstName, notification.ApplicationLastName, notification.ApplicationEmail);

            _contactRepository.Add(contact);

            _logger.LogInformation("New contact with '{ContactId}' and private email '{PrivateEmail}'.", contact.Id, contact.PrivateEmail.Value);
        }

        private async Task AssignContactToJobApplication(Contact contact, Guid jobApplicationId, CancellationToken cancellationToken)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(jobApplicationId);
            jobApplication.AssignContact(contact.Id);

            _jobApplicationRepository.Update(jobApplication);

            _logger.LogDebug($"Contact with id '{contact.Id}' assigned to Job Application with id '{jobApplicationId}'.");
        }
    }
}
