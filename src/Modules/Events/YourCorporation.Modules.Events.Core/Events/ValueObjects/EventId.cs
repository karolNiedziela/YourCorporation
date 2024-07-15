using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal class EventId : TypedIdValueBase
    {
        public EventId(Guid value) : base(value)
        {
        }

        public static implicit operator Guid(EventId id) => id.Value;

        public static implicit operator EventId(Guid value) => new(value);
    }
}
