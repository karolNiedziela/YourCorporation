namespace YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults
{
    internal record ContactJobApplicationResultId(Guid Value)
    {
        public static ContactJobApplicationResultId New() => new(Guid.NewGuid());

        public static implicit operator Guid(ContactJobApplicationResultId contactJobApplicationResultId) => contactJobApplicationResultId.Value;
    }
}
