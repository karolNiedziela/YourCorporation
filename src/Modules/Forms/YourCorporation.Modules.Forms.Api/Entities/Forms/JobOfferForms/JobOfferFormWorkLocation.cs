using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms
{
    internal class JobOfferFormWorkLocation : Entity
    {
        public Guid JobOfferFormId { get; set; }

        public Guid WorkLocationId { get; set; }
    }
}
