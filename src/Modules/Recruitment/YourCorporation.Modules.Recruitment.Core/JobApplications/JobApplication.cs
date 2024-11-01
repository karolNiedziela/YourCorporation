using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.WorkLocations;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications
{
    internal class JobApplication : AggregateRoot<JobApplicationId>
    {
        private readonly List<WorkLocationId> _chosenWorkLocations = [];

        private readonly List<RecruitmentQueueId> _recruitmentQueues = [];

        public string Name { get; private set; }

        public string CVUrl { get; private set; }

        public Guid JobOfferId { get; private set; }

        public Guid JobOfferSubmissionId { get; private set; }

        public ContactId ContactId { get; private set; }

        public AssignedRecruiter AssignedRecruiter { get; private set; } 

        private JobApplication() : base() { }

        public IReadOnlyCollection<WorkLocationId> ChosenWorkLocations => _chosenWorkLocations.AsReadOnly();

        public IReadOnlyCollection<RecruitmentQueueId> RecruitmentQueues => _recruitmentQueues.AsReadOnly();

        public JobApplication(
            string cvUrl,
            Guid jobOfferId,
            Guid jobOfferSubmissionId,
            FirstName applicationFirstName,
            LastName applicationLastName,
            PrivateEmail applicationEmail,
            IEnumerable<WorkLocationId> chosenWorkLocations,
            JobApplicationId jobApplicationId = null) : base(jobApplicationId ?? JobApplicationId.New())
        {
            CVUrl = cvUrl;
            JobOfferId = jobOfferId;
            Name = $"{applicationFirstName.Value} {applicationLastName.Value}";
            JobOfferSubmissionId = jobOfferSubmissionId;
            _chosenWorkLocations.AddRange(chosenWorkLocations);
            AddDomainEvent(new JobApplicationCreatedDomainEvent(Id, applicationFirstName.Value, applicationLastName.Value, applicationEmail.Value));
        }

        internal void AssignContact(ContactId contactId) => ContactId = contactId;

        internal void AssignRecruitmentQueues(IEnumerable<RecruitmentQueueId> recruitmentQueues)
            => _recruitmentQueues.AddRange(recruitmentQueues);

        public void AssignRecruiter(AssignedRecruiter assignedRecruiter) => AssignedRecruiter = assignedRecruiter;        
    }
}
