using YourCorporation.Shared.Abstractions.Commands;

namespace YourCorporation.Modules.Events.Application.Commands.Events.GoLive
{
    internal record GoLiveCommand(Guid EventId) : ICommand;
}
