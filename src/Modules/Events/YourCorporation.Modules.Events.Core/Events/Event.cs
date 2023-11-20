using YourCorporation.Modules.Events.Core.Attendees.ValueObjects;
using YourCorporation.Modules.Events.Core.Events.Entities;
using YourCorporation.Modules.Events.Core.Events.Enums;
using YourCorporation.Modules.Events.Core.Events.Events;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events
{
    internal class Event : AggregateRoot<EventId>
    {
        private List<DeclaredEventAttendee> _declaredAttendees = new();

        private List<ConfirmedEventAttendee> _confirmedAttendees = new();

        private List<WaitlistedEventAttendee> _waitlistedAttendees = new();

        private List<EventSpeaker> _speakers = new();

        public EventName Name { get; private set; } = default!;

        public EventCategory Category { get; private set; }

        public EventMode Mode { get; private set; }

        public EventStatus Status { get; private set; }

        public BegginingAndEndOfEvent BegginingAndEndOfEvent { get; private set; } = default!;

        public EventLimits EventLimits { get; private set; } = default!;

        public IReadOnlyCollection<DeclaredEventAttendee> DeclaredAttendees => _declaredAttendees.AsReadOnly();

        public IReadOnlyCollection<ConfirmedEventAttendee> ConfirmedAttendees => _confirmedAttendees.AsReadOnly();

        public IReadOnlyCollection<WaitlistedEventAttendee> WaitlistedAttendees => _waitlistedAttendees.AsReadOnly();

        public IReadOnlyCollection<EventSpeaker> Speakers => _speakers.AsReadOnly();

        private Event(): base() { }

        public Event(
            EventId id,
            EventName name,
            EventCategory category,
            EventMode mode,
            BegginingAndEndOfEvent begginingAndEndOfEvent,
            EventLimits eventLimits) : base(id)
        {
            Name = name;
            Category = category;
            Mode = mode;
            EventLimits = eventLimits;
            BegginingAndEndOfEvent = begginingAndEndOfEvent;
        }

        public void AddSpeaker(SpeakerId speakerId)
        {
            CheckIsInValidStatus(EventStatus.Draft);

            if (_speakers.Any(x => x.SpeakerId == speakerId))
            {
                throw new InvalidOperationException();
            }

            _speakers.Add(new EventSpeaker(Id, speakerId));
        }

        public void AddDeclaredParticipant(AttendeeId declaredAttendeeId, DateTimeOffset signUpDate)
        {
            CheckIsInValidStatus(EventStatus.Live);

            var existingDeclaredAttendee = _declaredAttendees.FirstOrDefault(x => x.AttendeeId == declaredAttendeeId);
            if (existingDeclaredAttendee is not null)
            {
                throw new InvalidOperationException();
            }

            var existingConfirmedAttendee = _confirmedAttendees.FirstOrDefault(x => x.AttendeeId == declaredAttendeeId);
            if (existingConfirmedAttendee is not null)
            {
                throw new InvalidOperationException();
            }

            if (EventLimits.AttendeesLimit.HasValue && _declaredAttendees.Count >= EventLimits?.AttendeesLimit.Value)
            {
                AddToWailist(declaredAttendeeId, signUpDate);
                return;
            }

            _declaredAttendees.Add(DeclaredEventAttendee.Create(declaredAttendeeId, Id, signUpDate));
            AddDomainEvent(new DeclaredEventAttendeeCreatedDomainEvent(Id, declaredAttendeeId));
        }

        public void ConfirmParticipation(AttendeeId declaredAttendeeId, DateTimeOffset confirmationTime)
        {
            CheckIsInValidStatus(EventStatus.Live);

            var declaredAttendee =
                _declaredAttendees.FirstOrDefault(x => x.AttendeeId == declaredAttendeeId) ??
                throw new InvalidOperationException();

            var confirmedAttendee = declaredAttendee.ConfirmParticipation(confirmationTime);

            _confirmedAttendees.Add(confirmedAttendee);
            _declaredAttendees.Remove(declaredAttendee);
            AddDomainEvent(new ConfirmedEventAttendeeCreatedDomainEvent(Id, confirmedAttendee.AttendeeId));
        }

        public void GoLive() 
        {
            CheckIsInValidStatus(EventStatus.Draft);

            Status = EventStatus.Live;
            AddDomainEvent(new EventWentLiveDomainEvent(Guid.NewGuid(), this));
        }

        private void AddToWailist(AttendeeId attendeeId, DateTimeOffset signUpDate)
        {
            var waitlistedAttendee = _waitlistedAttendees.FirstOrDefault(x => x.AttendeeId == attendeeId);
            if (waitlistedAttendee is not null)
            {
                throw new InvalidOperationException();
            }

            _waitlistedAttendees.Add(WaitlistedEventAttendee.Create(attendeeId, Id, signUpDate));
            AddDomainEvent(new WaitlistedEventAttendeeCreatedDomainEvent(Id, waitlistedAttendee!.AttendeeId));
        }

        private void CheckIsInValidStatus(EventStatus status) 
        {
            if (Status == status)
            {
                return;
            }

            throw new InvalidOperationException();
        }
    }
}
