using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations.Events
{
    internal record WorkLocationCreatedDomainEvent(Guid Id, WorkLocation WorkLocation) : DomainEvent(Id);
}
