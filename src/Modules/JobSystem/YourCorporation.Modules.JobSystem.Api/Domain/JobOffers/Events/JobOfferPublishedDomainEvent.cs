using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events
{
    internal record JobOfferPublishedDomainEvent(Guid Id, JobOffer JobOffer) : DomainEvent(Id);
}
