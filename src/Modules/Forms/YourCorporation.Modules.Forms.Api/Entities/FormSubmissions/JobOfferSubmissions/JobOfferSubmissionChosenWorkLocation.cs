using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions
{
    internal class JobOfferSubmissionChosenWorkLocation : Entity
    {
        public Guid JobOfferSubmissionId { get; set; }

        public Guid WorkLocationId { get; set; }
    }
}
