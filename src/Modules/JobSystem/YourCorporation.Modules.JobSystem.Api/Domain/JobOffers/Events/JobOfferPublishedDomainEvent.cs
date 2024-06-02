using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.JobOffers.Events
{
    internal record JobOfferPublishedDomainEvent(JobOffer JobOffer) : IDomainEvent;
}
