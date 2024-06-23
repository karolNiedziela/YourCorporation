using YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Constants;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications
{
    internal class JobApplication : AggregateRoot<JobApplicationId>
    {
        private readonly List<JobApplicationChosenWorkLocation> _chosenWorkLocations = [];

        public string Name { get; private set; }

        public string CVUrl { get; private set; }

        public JobApplicationStatus JobApplicationStatus { get; private set; }
          
        public JobOffer JobOffer { get; private set; }

        public Guid JobOfferSubmissionId { get; private set; }

        public CandidateId CandidateId { get; private set; }

        private JobApplication() : base() { }

        public IReadOnlyCollection<JobApplicationChosenWorkLocation> ChosenWorkLocations => _chosenWorkLocations.AsReadOnly();

        public JobApplication (
            string cvUrl,
            JobOffer jobOffer,
            Guid jobOfferSubmissionId,
            CandidateId candidateId,
            string CandidateFirstName,
            string CandidateLastName,
            IEnumerable<JobApplicationChosenWorkLocation> chosenWorkLocations,
            JobApplicationId jobApplicationId = null) : base(jobApplicationId ?? new JobApplicationId())
        {
            Name = $"{CandidateFirstName} {CandidateLastName}";
            CVUrl = cvUrl;
            JobOffer = jobOffer;
            JobOfferSubmissionId = jobOfferSubmissionId;
            CandidateId = candidateId;
            JobApplicationStatus = JobApplicationStatus.Created;
            _chosenWorkLocations.AddRange(chosenWorkLocations);
            AddDomainEvent(new JobApplicationCreatedDomainEvent(Id));
        }
    }
}
