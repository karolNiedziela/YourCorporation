namespace YourCorporation.Modules.Recruitment.Core.ContactStatuses
{
    internal record ContactStatusId(Guid Value)
    {
        public static ContactStatusId New() => new(Guid.NewGuid());

        public static implicit operator Guid(ContactStatusId contactStatusId) => contactStatusId.Value;
    }
}
