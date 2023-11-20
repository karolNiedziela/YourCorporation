using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Events.Core.Speakers
{
    internal class Speaker : AggregateRoot<SpeakerId>
    {
        public FirstName FirstName { get; private set; }

        public LastName LastName { get; private set; }

        private Speaker() : base() { }

        public Speaker(SpeakerId id, FirstName firstName, LastName lastName) : base (id)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
