using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.Events
{
    internal record EventWentLiveDomainEvent(
        Guid EventId,
        string EventName,
        DateTimeOffset BegginningOfEvent,
        DateTimeOffset EndOfEvent
        ) : IDomainEvent;

}
