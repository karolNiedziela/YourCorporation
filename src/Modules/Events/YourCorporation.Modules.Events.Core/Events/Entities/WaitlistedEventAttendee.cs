using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.Entities
{
    internal class WaitlistedEventAttendee : Entity
    {
        public AttendeeId AttendeeId { get; private set; } = default!;

        public EventId EventId { get; private set; } = default!;

        public DateTimeOffset SignUpDate { get; private set; }

        public bool IsMovedToDeclaredAttendee { get; private set; }

        public DateTimeOffset? MovedToDeclaredAttendee { get; private set; }

        private WaitlistedEventAttendee() { }

        private WaitlistedEventAttendee(AttendeeId attendeeId, EventId eventId, DateTimeOffset signUpDate)
        {
            AttendeeId = attendeeId;
            EventId = eventId;
            SignUpDate = signUpDate;
            IsMovedToDeclaredAttendee = false;
        }

        internal static WaitlistedEventAttendee Create(AttendeeId attendeeId, EventId eventId, DateTimeOffset signUpDate)
            => new(attendeeId, eventId, signUpDate);
    }
}
