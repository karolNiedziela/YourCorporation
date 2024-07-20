using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Constants;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Events;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Types;
using YourCorporation.Shared.Abstractions.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications
{
    internal class JobApplication : AggregateRoot<JobApplicationId>
    {
        private readonly List<JobApplicationChosenWorkLocation> _chosenWorkLocations = [];

        public string Name => $"{ApplicationFirstName.Value} {ApplicationLastName.Value}";

        public string CVUrl { get; private set; }

        public JobApplicationStatus JobApplicationStatus { get; private set; }
          
        public JobOffer JobOffer { get; private set; }

        public Guid JobOfferSubmissionId { get; private set; }

        public PrivateEmail ApplicationEmail { get; private set; }

        public FirstName ApplicationFirstName { get; private set; }

        public LastName ApplicationLastName { get; private set; }

        public ContactId ContactId { get; private set; }

        private JobApplication() : base() { }

        public IReadOnlyCollection<JobApplicationChosenWorkLocation> ChosenWorkLocations => _chosenWorkLocations.AsReadOnly();

        public JobApplication (
            string cvUrl,
            JobOffer jobOffer,
            Guid jobOfferSubmissionId,
            FirstName applicationFirstName,
            LastName applicationLastName,
            PrivateEmail applicationEmail,
            IEnumerable<JobApplicationChosenWorkLocation> chosenWorkLocations,
            JobApplicationId jobApplicationId = null) : base(jobApplicationId)
        {            
            CVUrl = cvUrl;
            JobOffer = jobOffer;
            ApplicationFirstName = applicationFirstName;
            ApplicationLastName = applicationLastName;
            ApplicationEmail = applicationEmail;
            JobOfferSubmissionId = jobOfferSubmissionId;
            JobApplicationStatus = JobApplicationStatus.Created;
            _chosenWorkLocations.AddRange(chosenWorkLocations);
            AddDomainEvent(new JobApplicationCreatedDomainEvent(Id, ApplicationFirstName.Value, ApplicationLastName.Value, ApplicationEmail.Value));
        }

        public void AssignContact(ContactId contactId)
        {
            ContactId = contactId;
            JobApplicationStatus = JobApplicationStatus.ReadyToProcess;
        }
    }
}
