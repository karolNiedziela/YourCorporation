using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.ContactStatuses
{
    internal class ContactStatus : Entity<ContactStatusId>
    {
        public static readonly ContactStatus ApplicantNotVerified = new("Applicant", "Not Verified", new ContactStatusId(Guid.Parse("0380BC27-18DE-4683-BD10-37C267F4F979")));
        public static readonly ContactStatus CandidateToContact = new("Candidate", "To contact", new ContactStatusId(Guid.Parse("92845A34-6931-4316-AEC9-A9DD2799F3AD")));
        public static readonly ContactStatus CandidateRejected = new("Candidate", "Rejected", new ContactStatusId(Guid.Parse("CCC70EE3-3F85-4DF1-9761-9D5D1EFF54E5")));

        public string Status { get; private set; }

        public string Substatus { get; private set; }

        private ContactStatus() { }

        private ContactStatus(string status, string substatus, ContactStatusId contactStatusId) : base(contactStatusId)
        {
            Status = status;
            Substatus = substatus;
        }

        public static IEnumerable<ContactStatus> GetAll() => [
            ApplicantNotVerified,
            CandidateToContact,
            CandidateRejected,
        ];
    }
}
