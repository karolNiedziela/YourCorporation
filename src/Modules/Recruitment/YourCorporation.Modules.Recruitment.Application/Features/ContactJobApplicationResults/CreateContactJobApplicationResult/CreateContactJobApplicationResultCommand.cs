using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Constants;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Application.Features.ContactJobApplicationResults.CreateContactJobApplicationResult
{
    internal record CreateContactJobApplicationResultCommand(
        Guid JobApplicationId, 
        Guid ContactId,
        ApplicationDecision ApplicationDecision, 
        RejectedReason? RejectedReason) : ICommand<Result<Guid>>;
}
