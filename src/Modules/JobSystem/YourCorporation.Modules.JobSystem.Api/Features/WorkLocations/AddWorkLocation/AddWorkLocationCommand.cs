using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.WorkLocations.AddWorkLocation
{
    internal record AddWorkLocationCommand(string Name, string Code, Guid? Id = null) : ICommand<Result<Guid>>;
}
