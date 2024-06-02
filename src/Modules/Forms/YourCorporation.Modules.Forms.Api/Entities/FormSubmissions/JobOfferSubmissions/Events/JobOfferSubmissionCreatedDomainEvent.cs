using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Events
{
    internal record JobOfferSubmissionCreatedDomainEvent(JobOfferSubmission JobOfferSubmission) : IDomainEvent;
}
