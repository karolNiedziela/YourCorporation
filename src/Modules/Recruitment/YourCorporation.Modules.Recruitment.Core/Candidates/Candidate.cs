using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Candidates
{
    internal class Candidate : AggregateRoot<CandidateId>
    {
        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        public string FullName => FirstName + " " + LastName;

        public PrivateEmail PrivateEmail { get; private set; }

        public PrivatePhone PrivatePhone { get; private set; }

        public BirthDate BirthDate { get; private set; }

        public LinkedinUrl LinkedinUrl { get; private set; }

        //public Nationality Nationality { get; private set; }

        //public Country Country { get; private set; }

        //public string City { get; private set; }

        private Candidate() { }

        private Candidate(FirstName firstName, LastName lastName, PrivateEmail privateEmail, CandidateId candidateId = null) 
            : base(candidateId ?? new CandidateId(Guid.NewGuid()))
        {
            FirstName = firstName;
            LastName = lastName;
            PrivateEmail = privateEmail;
        }

        public static Candidate Create(FirstName firstName, LastName lastName, PrivateEmail privateEmail)
            => new(firstName, lastName, privateEmail);
    }
}
