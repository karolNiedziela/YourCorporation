using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.Events
{
    internal record DeclaredEventAttendeeCreatedDomainEvent(Guid EventId, Guid AttendeeId) : IDomainEvent;
}
