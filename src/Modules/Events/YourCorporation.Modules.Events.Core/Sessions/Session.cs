using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Modules.Events.Core.Sessions.Entities;
using YourCorporation.Modules.Events.Core.Sessions.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Sessions
{
    internal class Session : AggregateRoot<SessionId>
    {
        private List<SessionSpeaker> _speakers = new();

        public EventId EventId { get; private set; } = default!;

        public SessionName Name { get; private set; } = default!;

        public BegginingAndEndOfSession BegginingAndEndOfSession { get; private set; } = default!;

        public IReadOnlyCollection<SessionSpeaker> Speakers => _speakers.AsReadOnly();

        private Session(): base() { }

        public Session(SessionId id, EventId eventId, SessionName name, BegginingAndEndOfSession begginingAndEndOfSession) : base (id)
        {            
            EventId = eventId;
            Name = name;
            BegginingAndEndOfSession = begginingAndEndOfSession;
        }

        public void AddSpeaker(SpeakerId speakerId)
        {
            if (_speakers.Any(x => x.SpeakerId == speakerId))
            {
                throw new InvalidOperationException();
            }

            _speakers.Add(new SessionSpeaker(Id, speakerId));
        }
    }
}
