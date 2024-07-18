using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Attendees.ValueObjects
{
    internal class AttendeeId(Guid Value) : StronglyTypedId(Value)
    {
    }
}
