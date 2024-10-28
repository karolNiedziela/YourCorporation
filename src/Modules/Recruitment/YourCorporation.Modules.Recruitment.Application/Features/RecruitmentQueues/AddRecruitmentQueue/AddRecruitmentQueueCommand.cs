using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Application.Features.RecruitmentQueues.AddRecruitmentQueue
{
    internal record AddRecruitmentQueueCommand(string Name, List<Guid> WorkLocationIds) : ICommand<Result<Guid>>;
}
