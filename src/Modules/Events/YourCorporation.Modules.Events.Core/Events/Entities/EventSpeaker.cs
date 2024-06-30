using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.Entities
{
    internal class EventSpeaker : Entity
    {
        public EventId EventId { get; private set; } = default!;

        public SpeakerId SpeakerId { get; private set; } = default!;

        private EventSpeaker() { }

        internal EventSpeaker(EventId eventId, SpeakerId speakerId)
        {
            EventId = eventId;
            SpeakerId = speakerId;
        }
    }
}
