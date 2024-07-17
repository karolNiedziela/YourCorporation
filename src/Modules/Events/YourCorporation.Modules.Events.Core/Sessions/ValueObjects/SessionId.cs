using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal class SessionId(Guid Value) : StronglyTypedId(Value)
    {
    }
}
