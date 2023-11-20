using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Events.Core.Events.Events
{
    internal record EventWentLiveDomainEvent(Guid Id, Event @Event) : DomainEvent(Id);
}
