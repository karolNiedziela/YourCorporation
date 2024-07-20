using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.Contacts.Repositories;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

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
            var privateEmail = PrivateEmail.Create(notification.ApplicationEmail);

            var existingContact = await _contactRepository.GetAsync(privateEmail.Value);
            if (existingContact is not null)
            {
                await AssignContactToJobApplication(existingContact, notification, cancellationToken);
                return;
            }

            var contact = Contact.CreateFromJobApplication(
                FirstName.Create(notification.ApplicationFirstName).Value,
                LastName.Create(notification.ApplicationLastName).Value,
                privateEmail.Value,
                new JobApplicationId(notification.JobApplicationId));

            _contactRepository.Add(contact);

            _logger.LogInformation("New contact with '{ContactId}' and private email '{PrivateEmail}'.", contact.Id, contact.PrivateEmail.Value);
        }

        private async Task AssignContactToJobApplication(Contact contact, JobApplicationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(notification.JobApplicationId);
            jobApplication.AssignContact(contact.Id);

            _jobApplicationRepository.Update(jobApplication);

            _logger.LogDebug($"Contact with id '{contact.Id}' assigned to Job Application with id '{notification.JobApplicationId}'.");
        }
    }
}
