using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms
{
    internal class JobOfferForm : FormBase
    {
        public Guid JobOfferId { get; private set; }

        public List<JobOfferSubmission> Submissions { get; private set; } = [];

        public List<JobOfferFormWorkLocation> JobOfferFormWorkLocations { get; private set; } = [];

        public List<WorkLocation> WorkLocations { get; private set; } = []; 

        private JobOfferForm() : base() { }

        public JobOfferForm(
            Guid id,
            Guid jobOfferId,
            string jobOfferName,
            List<JobOfferFormWorkLocation> workLocations
            ) : base(id, jobOfferName, true)
        {
            JobOfferId = jobOfferId;
            JobOfferFormWorkLocations = workLocations;
        }
    }
}
