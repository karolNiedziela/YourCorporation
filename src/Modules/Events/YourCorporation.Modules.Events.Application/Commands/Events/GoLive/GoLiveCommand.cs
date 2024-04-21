using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal record GoLiveCommand(Guid EventId) : ICommand<Result>;
}
