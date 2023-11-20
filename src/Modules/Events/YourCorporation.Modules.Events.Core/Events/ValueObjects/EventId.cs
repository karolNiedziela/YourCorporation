namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record struct EventId
    {
        public Guid Value { get; }

        public EventId()
        {
            Value = Guid.NewGuid();
        }

        public EventId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(EventId id) => id.Value;
    }
}
