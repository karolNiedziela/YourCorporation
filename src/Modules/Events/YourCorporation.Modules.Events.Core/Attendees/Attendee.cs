using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Shared.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Events.Core.Attendees
{
    internal class Attendee : AggregateRoot<AttendeeId>
    {
        public Email Email { get; private set; } = default!;

        public FirstName FirstName { get; private set; } = default!;

        public LastName LastName { get; private set; } = default!;

        private Attendee() : base() { }

        public Attendee(AttendeeId id, FirstName firstName, LastName lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
