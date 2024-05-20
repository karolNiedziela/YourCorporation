using YourCorporation.Modules.Forms.Api.Entities.Forms.JobOfferForms;
using YourCorporation.Modules.Forms.Api.Entities.WorkLocations;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions
{
    internal class JobOfferSubmission : FormSubmissionBase
    {
        public JobOfferForm JobOfferForm { get; private set; }

        public Guid JobOfferFormId { get; private set; }

        public List<JobOfferSubmissionChosenWorkLocation> JobOfferSubmissionChosenWorkLocations { get; private set; } = [];

        public List<WorkLocation> ChosenWorkLocations { get; private set; } = [];

        private JobOfferSubmission() : base(){ }

        public JobOfferSubmission(
            Guid id,
            Guid jobOfferFormId,
            string firstName,
            string lastName,
            string email,
            List<JobOfferSubmissionChosenWorkLocation> jobOfferSubmissionChosenWorkLocations)
            : base(id, firstName, lastName, email)
        {
            JobOfferFormId = jobOfferFormId;
            JobOfferSubmissionChosenWorkLocations = jobOfferSubmissionChosenWorkLocations;
        }
    }
}
