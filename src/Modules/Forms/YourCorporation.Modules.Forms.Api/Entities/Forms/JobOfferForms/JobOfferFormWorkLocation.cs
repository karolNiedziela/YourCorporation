using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms
{
    internal class JobOfferFormWorkLocation
    {
        public Guid JobOfferFormId { get; set; }

        public JobOfferForm JobOfferForm { get; set; }

        public Guid WorkLocationId { get; set; }

        public WorkLocation WorkLocation { get; set; }
    }
}
