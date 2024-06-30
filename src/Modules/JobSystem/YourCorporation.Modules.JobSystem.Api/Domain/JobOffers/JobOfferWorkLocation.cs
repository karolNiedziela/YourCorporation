using YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.JobSystem.Api.Domain.JobOffers
{
    internal class JobOfferWorkLocation : Entity
    {
        public Guid JobOfferId { get; set; }

        public Guid WorkLocationId { get; set; }
    }
}
