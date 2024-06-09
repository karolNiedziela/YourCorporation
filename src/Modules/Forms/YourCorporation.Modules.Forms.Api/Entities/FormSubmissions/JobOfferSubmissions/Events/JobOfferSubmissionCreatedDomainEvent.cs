using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.JobOfferSubmissions.Events
{
    internal record JobOfferSubmissionCreatedDomainEvent(
        Guid JobOfferSubmissionId,
        string FirstName,
        string LastName,
        string CvUrl,
        string Email,
        IEnumerable<Guid> WorkLocationIds,
        Guid JobOfferId) : IDomainEvent;
}
