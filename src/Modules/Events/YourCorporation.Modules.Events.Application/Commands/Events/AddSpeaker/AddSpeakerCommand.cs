using MediatR;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Application.Commands.Events.AddSpeaker
{
    internal record AddSpeakerCommand(Guid EventId, Guid SpeakerId) : ICommand<Result>;
}
