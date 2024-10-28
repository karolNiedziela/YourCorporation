using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.ContactStatuses
{
    internal class ContactStatus : Entity<ContactStatusId>
    {
        public static readonly ContactStatus ApplicantNotVerified = new("Applicant", "Not Verified", new ContactStatusId(Guid.Parse("0380BC27-18DE-4683-BD10-37C267F4F979")));

        public string Status { get; private set; }

        public string Substatus { get; private set; }

        private ContactStatus() { }

        private ContactStatus(string status, string substatus, ContactStatusId contactStatusId) : base(contactStatusId)
        {
            Status = status;
            Substatus = substatus;
        }

        public static IEnumerable<ContactStatus> GetAll() => [ApplicantNotVerified];
    }
}
