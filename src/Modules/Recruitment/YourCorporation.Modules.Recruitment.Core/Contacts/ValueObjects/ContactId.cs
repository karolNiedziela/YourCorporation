namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal record ContactId(Guid Value)
    {
        public static ContactId New() => new(Guid.NewGuid());

        public static implicit operator Guid(ContactId contactId) => contactId.Value;
    }
}
