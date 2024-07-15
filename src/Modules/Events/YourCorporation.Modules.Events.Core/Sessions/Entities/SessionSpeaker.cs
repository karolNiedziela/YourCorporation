using YourCorporation.Modules.Events.Core.Sessions.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;

namespace YourCorporation.Modules.Events.Core.Sessions.Entities
{
    internal class SessionSpeaker
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
