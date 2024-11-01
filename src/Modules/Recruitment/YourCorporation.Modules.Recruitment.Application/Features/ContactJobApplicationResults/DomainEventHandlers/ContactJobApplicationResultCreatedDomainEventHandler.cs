using MediatR;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Constants;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Events;
using YourCorporation.Modules.Recruitment.Core.Contacts.Repositories;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.ContactStatuses;

namespace YourCorporation.Modules.Recruitment.Application.Features.ContactJobApplicationResults.DomainEventHandlers
{
    internal class ContactJobApplicationResultCreatedDomainEventHandler : INotificationHandler<ContactJobApplicationResultCreatedDomainEvent>
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactJobApplicationResultCreatedDomainEventHandler> _logger;

        public ContactJobApplicationResultCreatedDomainEventHandler(IContactRepository contactRepository, ILogger<ContactJobApplicationResultCreatedDomainEventHandler> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task Handle(ContactJobApplicationResultCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var existingContact = await _contactRepository.GetAsync(new ContactId(notification.ContactId));
            if (existingContact is null)
            {
                _logger.LogInformation("Contact with id '{ContactId}' was not found.", notification.ContactId);
                return;
            }

            switch (notification.ApplicationDecision)
            {
                case ApplicationDecision.ToContact:
                    existingContact.UpdateStatus(ContactStatus.CandidateToContact.Id);
                    break;

                case ApplicationDecision.Rejected:
                    existingContact.UpdateStatus(ContactStatus.CandidateRejected.Id);
                    break;

                default:
                    break;
            }

            _contactRepository.Update(existingContact);

            _logger.LogInformation("Contact status updated to '{ContactStatus}' for contact with id '{ContactId}'.", 
                existingContact.ContactStatusId,
                notification.ContactId);
        }
    }
}
