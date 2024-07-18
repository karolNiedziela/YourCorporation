using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.ContactStatuses
{
    internal class ContactStatusId : StronglyTypedId
    {
        public ContactStatusId() : base()
        {
        }

        public ContactStatusId(Guid value) : base(value)
        {
        }
    }
}
