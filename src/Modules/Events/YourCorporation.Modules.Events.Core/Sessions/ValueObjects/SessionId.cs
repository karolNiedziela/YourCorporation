using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal class SessionId : StronglyTypedId
    {
        public SessionId() : base()
        {
        }

        public SessionId(Guid value) : base(value)
        {
        }
    }
}
