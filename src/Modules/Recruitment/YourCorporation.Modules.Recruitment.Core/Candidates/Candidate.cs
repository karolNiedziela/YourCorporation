using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Candidates
{
    internal class Candidate : AggregateRoot<CandidateId>
    {
        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string FullName => FirstName.Value + " " + LastName.Value;

        public PrivateEmail PrivateEmail { get; private set; }

        public PrivatePhone PrivatePhone { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public LinkedinUrl LinkedinUrl { get; private set; }

        //public JobApplicationCandidate JobApplicationContact { get; private set; }

        //public Nationality Nationality { get; private set; }

        //public Country Country { get; private set; }

        public string City { get; private set; }
    }
}
