namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal readonly record struct ContactId(Guid Value)
    {
        public static ContactId New() => new(Guid.NewGuid());

        public static implicit operator Guid(ContactId contactId) => contactId.Value;
    }
}
