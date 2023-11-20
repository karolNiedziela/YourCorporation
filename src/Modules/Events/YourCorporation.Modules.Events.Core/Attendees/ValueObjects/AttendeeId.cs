namespace YourCorporation.Modules.Events.Core.Attendees.ValueObjects
{
    internal record struct AttendeeId
    {
        public Guid Value { get; }

        public AttendeeId()
        {
            Value = Guid.NewGuid();
        }

        public AttendeeId(Guid value)
        {
            Value = value;
        }

        public static implicit operator Guid(AttendeeId id) => id.Value;
    }
}
