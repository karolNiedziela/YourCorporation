using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;

namespace YourCorporation.Modules.Events.Core.Events.Entities
{
    internal class DeclaredEventAttendee
    {
        public AttendeeId AttendeeId { get; private set; } = default!;

        public EventId EventId { get; private set; } = default!;

        public DateTimeOffset SignUpDate { get; private set; }

        private DeclaredEventAttendee() { }

        private DeclaredEventAttendee(AttendeeId attendeeId, EventId eventId, DateTimeOffset signUpDate)
        {
            AttendeeId = attendeeId;
            EventId = eventId;
            SignUpDate = signUpDate;
        }

        internal static DeclaredEventAttendee Create(AttendeeId attendeeId, EventId eventId, DateTimeOffset signUpDate)
            => new(attendeeId, eventId, signUpDate);

        internal ConfirmedEventAttendee ConfirmParticipation(DateTimeOffset confirmationDate)
        {
            return ConfirmedEventAttendee.Create(this, confirmationDate);
        }
    }
}
