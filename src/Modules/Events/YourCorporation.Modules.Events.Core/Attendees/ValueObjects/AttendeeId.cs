namespace YourCorporation.Modules.Events.Core.Attendees.ValueObjects
{
    internal readonly record struct AttendeeId(Guid Value)
    {
        public static AttendeeId New() => new(Guid.NewGuid());

        public static implicit operator Guid(AttendeeId attendeeId) => attendeeId.Value;
    }
}
