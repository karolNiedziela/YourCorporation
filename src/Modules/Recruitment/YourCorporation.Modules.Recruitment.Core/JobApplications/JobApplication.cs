using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Constants;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications
{

    internal class JobApplication : AggregateRoot<JobApplicationId>
    {
        //private List<WorkLocation> _chosenWorkLocations = [];

        public string Name { get; private set; }

        public string CVUrl { get; private set; }

        public JobApplicationStatus JobApplicationStatus { get; private set; }
          
        public JobOffer JobOffer { get; private set; }

        public CandidateId CandidateId { get; private set; }

        //public IReadOnlyCollection<WorkLocation> ChosenWorkLocations => _chosenWorkLocations.AsReadOnly();

        private JobApplication() : base() { }

        public JobApplication
            (
            JobApplicationId jobApplicationId,
            string name,
            string cvUrl,          
            JobOffer jobOffer,
            CandidateId candidateId) : base(jobApplicationId)
        {
           
            Name = name;
            CVUrl = cvUrl;
            JobOffer = jobOffer;
            CandidateId = candidateId;
        }
    }
}
