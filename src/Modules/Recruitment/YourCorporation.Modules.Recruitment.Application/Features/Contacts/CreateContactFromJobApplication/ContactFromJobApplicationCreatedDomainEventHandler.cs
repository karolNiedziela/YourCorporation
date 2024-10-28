using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.Contacts.Events;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Services;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Application.Features.Contacts.CreateContactFromJobApplication
{
    internal class ContactFromJobApplicationCreatedDomainEventHandler : INotificationHandler<ContactFromJobApplicationCreatedDomainEvent>
    {
        private readonly IJobApplicationService _jobApplicationService;
        private readonly ILogger<ContactFromJobApplicationCreatedDomainEventHandler> _logger;

        public ContactFromJobApplicationCreatedDomainEventHandler(IJobApplicationService jobApplicationService, ILogger<ContactFromJobApplicationCreatedDomainEventHandler> logger)
        {
            _jobApplicationService = jobApplicationService;
            _logger = logger;
        }

        public async Task Handle(ContactFromJobApplicationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _jobApplicationService.ProcessNewJobApplication(new JobApplicationId(notification.JobApplicationId), new ContactId(notification.ContactId));

            _logger.LogInformation("Contact with id '{ContactId}' assigned to Job Application with id '{JobApplicationId}'.", notification.ContactId, notification.JobApplicationId);
        }
    }
}
