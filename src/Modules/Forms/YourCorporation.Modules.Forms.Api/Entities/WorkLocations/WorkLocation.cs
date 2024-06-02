using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.WorkLocations
{
    internal class WorkLocation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public IEnumerable<JobOfferFormWorkLocation> JobOfferFormWorkLocations { get; set; } = [];

        public IEnumerable<JobOfferForm> JobOfferForms { get; set; } = [];

        public List<JobOfferSubmissionChosenWorkLocation> JobOfferSubmissionChosenWorkLocations { get; set; } = [];

        public List<JobOfferSubmission> JobOfferSubmissions { get; set; } = [];
    }
}
