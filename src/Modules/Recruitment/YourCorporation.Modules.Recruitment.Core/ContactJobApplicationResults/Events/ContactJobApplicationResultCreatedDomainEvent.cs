using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Constants;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Events
{
    internal record ContactJobApplicationResultCreatedDomainEvent(
        Guid JobApplicationId,
        Guid ContactId,
        ApplicationDecision ApplicationDecision,
        RejectedReason? RejectedReason) : IDomainEvent;
}
