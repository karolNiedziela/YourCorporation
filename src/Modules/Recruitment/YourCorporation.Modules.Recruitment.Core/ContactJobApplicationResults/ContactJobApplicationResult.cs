using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Constants;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Events;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults
{
    internal class ContactJobApplicationResult : AggregateRoot<ContactJobApplicationResultId>
    {
        public ApplicationDecision ApplicationDecision { get; private set; }

        public RejectedReason? RejectedReason { get; private set; }

        public JobApplicationId JobApplicationId { get; private set; }

        public ContactId ContactId { get; private set; }

        private ContactJobApplicationResult() { }

        private ContactJobApplicationResult(JobApplicationId jobApplicationId,
            ContactId contactId,
            ApplicationDecision applicationDecision, 
            RejectedReason? rejectedReason) : base(ContactJobApplicationResultId.New())
        {
            JobApplicationId = jobApplicationId;
            ContactId = contactId;
            ApplicationDecision = applicationDecision;
            RejectedReason = rejectedReason;
            AddDomainEvent(new ContactJobApplicationResultCreatedDomainEvent(
                jobApplicationId.Value,
                contactId.Value,
                applicationDecision,
                rejectedReason));
        }
        
        public static Result<ContactJobApplicationResult> Create(
            JobApplicationId jobApplicationId,
            ContactId contactId,
            ApplicationDecision applicationDecision, 
            RejectedReason? rejectedReason) 
        {
            var rejectedReasonGivenForApplicationDecisionToContact = applicationDecision == ApplicationDecision.ToContact &&
                rejectedReason.HasValue;
            if (rejectedReasonGivenForApplicationDecisionToContact)
            {
                return ErrorCodes.ContactJobApplicationResult.GivenRejectedReasonError;
            }

            var noRejectedReasonForRejectedApplicationDecision = applicationDecision == ApplicationDecision.Rejected && !rejectedReason.HasValue;
            if (noRejectedReasonForRejectedApplicationDecision)
            {
                return ErrorCodes.ContactJobApplicationResult.NoRejectedReasonError;
            }
            
            return new ContactJobApplicationResult(jobApplicationId, contactId, applicationDecision, rejectedReason);
        }
    }
}
