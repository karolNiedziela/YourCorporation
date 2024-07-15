using YourCorporation.Modules.Recruitment.Core.Contacts.Entities;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Contacts
{
    internal class Contact : AggregateRoot<Guid>
    {
        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string FullName => FirstName + " " + LastName;

        public PrivateEmail PrivateEmail { get; private set; }

        public PrivatePhone PrivatePhone { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public LinkedinUrl LinkedinUrl { get; private set; }

        public ContactStatus ContactStatus { get; private set; }

        //public Nationality Nationality { get; private set; }

        //public Country Country { get; private set; }

        //public string City { get; private set; }

        private Contact() { }

        private Contact(FirstName firstName, LastName lastName, PrivateEmail privateEmail, Guid? contactId = null) 
            : base(contactId ?? Guid.NewGuid())
        {
            FirstName = firstName;
            LastName = lastName;
            PrivateEmail = privateEmail;
            ContactStatus = ContactStatus.ApplicantNotVerified;
        }

        public static Contact Create(FirstName firstName, LastName lastName, PrivateEmail privateEmail)
            => new(firstName, lastName, privateEmail);
    }
}
