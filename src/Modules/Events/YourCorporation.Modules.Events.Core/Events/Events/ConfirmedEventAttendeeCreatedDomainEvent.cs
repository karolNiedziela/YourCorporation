using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.Events
{
    internal record ConfirmedEventAttendeeCreatedDomainEvent(Guid EventId, Guid AttendeeId) : IDomainEvent;
}
