using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal class EventId(Guid value) : StronglyTypedId(value)
    {
        public static implicit operator EventId(Guid value) => new(value);
    }
}
