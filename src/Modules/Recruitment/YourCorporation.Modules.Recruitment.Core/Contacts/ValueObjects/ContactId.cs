using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal class ContactId : StronglyTypedId
    {
        public ContactId() : base()
        {
        }

        public ContactId(Guid value) : base(value)
        {
        }
    }
}
