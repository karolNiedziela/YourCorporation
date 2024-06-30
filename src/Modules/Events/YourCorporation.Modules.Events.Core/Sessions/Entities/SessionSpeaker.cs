using YourCorporation.Modules.Events.Core.Sessions.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Sessions.Entities
{
    internal class SessionSpeaker : Entity
    {
        public SessionId SessionId { get; private set; } = default!;

        public SpeakerId SpeakerId { get; private set; } = default!;

        private SessionSpeaker() { }

        internal SessionSpeaker(SessionId sessionId, SpeakerId speakerId)
        {
            SessionId = sessionId;
            SpeakerId = speakerId;
        }
    }
}
