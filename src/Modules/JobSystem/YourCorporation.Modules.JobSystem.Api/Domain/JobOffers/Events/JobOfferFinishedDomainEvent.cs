using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events
{
    internal record JobOfferFinishedDomainEvent(Guid Id, Guid JobOfferId) : DomainEvent(Id);
}
