using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal class ContactId(Guid value) : StronglyTypedId(value)
    {
    }
}
