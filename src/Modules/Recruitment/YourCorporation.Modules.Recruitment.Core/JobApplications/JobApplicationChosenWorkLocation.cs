using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications
{
    internal class JobApplicationChosenWorkLocation
    {
        public JobApplicationId JobApplicationId { get; set; }

        public WorkLocationId WorkLocationId { get; set; }

        private JobApplicationChosenWorkLocation() { }

        public JobApplicationChosenWorkLocation(JobApplicationId jobApplicationId, WorkLocationId workLocationId)
        {
            JobApplicationId = jobApplicationId;
            WorkLocationId = workLocationId;
        }
    }
}
