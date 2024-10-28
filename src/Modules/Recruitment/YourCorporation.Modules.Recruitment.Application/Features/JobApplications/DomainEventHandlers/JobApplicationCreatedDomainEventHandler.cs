using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.Contacts.Repositories;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Services;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Application.Features.JobApplications.DomainEventHandlers
{
    internal class JobApplicationCreatedDomainEventHandler : INotificationHandler<JobApplicationCreatedDomainEvent>
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<JobApplicationCreatedDomainEventHandler> _logger;
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationCreatedDomainEventHandler(
            IContactRepository contactRepository,
            ILogger<JobApplicationCreatedDomainEventHandler> logger,
            IJobApplicationService jobApplicationService)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _jobApplicationService = jobApplicationService;
        }

        public async Task Handle(JobApplicationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var privateEmail = PrivateEmail.Create(notification.ApplicationEmail);

            var existingContact = await _contactRepository.GetAsync(privateEmail.Value);
            if (existingContact is not null)
            {
                await _jobApplicationService.ProcessNewJobApplication(new JobApplicationId(notification.JobApplicationId), new ContactId(existingContact.Id));
                _logger.LogInformation("Contact with id '{ContactId}' assigned to Job Application with id '{JobApplicationId}'.", existingContact.Id, notification.JobApplicationId);
                return;
            }

            var contact = Contact.CreateFromJobApplication(
                FirstName.Create(notification.ApplicationFirstName).Value,
                LastName.Create(notification.ApplicationLastName).Value,
                privateEmail.Value,
                new JobApplicationId(notification.JobApplicationId));

            _contactRepository.Add(contact);
        }
    }
}
