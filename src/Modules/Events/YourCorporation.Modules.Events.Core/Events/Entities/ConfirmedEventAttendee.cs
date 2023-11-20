using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;

namespace YourCorporation.Modules.Events.Core.Events.Entities
{
    internal class ConfirmedEventAttendee
    {
        public AttendeeId AttendeeId { get; private set; } = default!;

        public EventId EventId { get; private set; } = default!;

        public DateTimeOffset SignUpDate { get; private set; }

        public DateTimeOffset ConfirmationDate { get; private set; }

        private ConfirmedEventAttendee() { }

        private ConfirmedEventAttendee(DeclaredEventAttendee declaredEventAttendee, DateTimeOffset confirmationDate)
        {
            AttendeeId = declaredEventAttendee.AttendeeId;
            EventId = declaredEventAttendee.EventId;
            SignUpDate = declaredEventAttendee.SignUpDate;
            ConfirmationDate = confirmationDate;
        }

        internal static ConfirmedEventAttendee Create(DeclaredEventAttendee declaredEventAttendee, DateTimeOffset confirmationDate)
            => new(declaredEventAttendee, confirmationDate);
    }
}
