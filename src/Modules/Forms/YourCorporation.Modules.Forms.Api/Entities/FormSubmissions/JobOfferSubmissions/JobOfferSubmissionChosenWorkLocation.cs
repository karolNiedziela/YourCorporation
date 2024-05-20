using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions
{
    internal class JobOfferSubmissionChosenWorkLocation
    {
        public Guid JobOfferSubmissionId { get; set; }

        public JobOfferSubmission JobOfferSubmission { get; set; }

        public Guid WorkLocationId { get; set; }

        public WorkLocation WorkLocation { get; set; }
    }
}
