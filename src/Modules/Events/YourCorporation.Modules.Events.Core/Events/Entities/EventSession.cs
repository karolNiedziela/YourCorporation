using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Modules.Events.Core.Sessions.ValueObjects;

namespace YourCorporation.Modules.Events.Core.Events.Entities
{
    internal class EventSession
    {
        public EventId EventId { get; private set; } = default!;

        public SessionId SessionId { get; private set; } = default!;

        internal EventSession(EventId eventId, SessionId sessionId)
        {
            EventId = eventId;
            SessionId = sessionId;
        }
    }
}
