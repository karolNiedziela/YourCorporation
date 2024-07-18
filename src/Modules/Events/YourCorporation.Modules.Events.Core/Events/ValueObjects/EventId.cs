using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal class EventId : StronglyTypedId
    {
        public EventId() : base() { }

        public EventId(Guid value) : base(value) { }
    }
}
