namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal readonly record struct EventId(Guid Value)
    {
        public static EventId New() => new(Guid.NewGuid());

        public static implicit operator Guid(EventId eventId) => eventId.Value;
    }
}
