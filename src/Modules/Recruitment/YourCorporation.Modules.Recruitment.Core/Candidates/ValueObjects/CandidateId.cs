namespace YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects
{
    internal record CandidateId
    {
        public Guid Value { get; }

        public CandidateId()
        {
            Value = Guid.NewGuid();
        }

        public CandidateId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(CandidateId id) => id.Value;
    }
}
