using YourCorporation.Modules.Recruitment.Core.Contacts.Events;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.ContactStatuses;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Contacts
{
    internal class Contact : AggregateRoot<ContactId>
    {
        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string FullName => FirstName + " " + LastName;

        public PrivateEmail PrivateEmail { get; private set; }

        public PrivatePhone PrivatePhone { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public LinkedinUrl LinkedinUrl { get; private set; }

        public ContactStatus ContactStatus { get; private set; }

        public ContactStatusId ContactStatusId { get; private set; }

        //public Nationality Nationality { get; private set; }

        //public Country Country { get; private set; }

        //public string City { get; private set; }

        private Contact() { }

        private Contact(FirstName firstName, LastName lastName, PrivateEmail privateEmail, JobApplicationId jobApplicationId, ContactId? contactId = null) 
            : base(contactId ?? ContactId.New())
        {
            FirstName = firstName;
            LastName = lastName;
            PrivateEmail = privateEmail;
            ContactStatusId = ContactStatus.ApplicantNotVerified.Id;
            AddDomainEvent(new ContactFromJobApplicationCreatedDomainEvent(Id, jobApplicationId.Value));
        }

        public static Contact CreateFromJobApplication(FirstName firstName, LastName lastName, PrivateEmail privateEmail, JobApplicationId jobApplicationId)
            => new(firstName, lastName, privateEmail, jobApplicationId);
    }
}
