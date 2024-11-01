using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Application.Features.JobApplications.AssignRecruiter
{
    internal record AssignRecruiterCommand(Guid JobApplicationId) : ICommand<Result<AssignRecruiterResponse>>;
}
