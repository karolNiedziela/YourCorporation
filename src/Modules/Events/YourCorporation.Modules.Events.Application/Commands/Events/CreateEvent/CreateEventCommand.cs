using YourCorporation.Modules.Events.Core.Events.Enums;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Application.Commands.Events.CreateEvent
{
    internal record CreateEventCommand(
         string Name,
         string Description,
         EventCategory Category,
         EventMode Mode,
         DateTimeOffset StartTime,
         DateTimeOffset EndTime,
         int? AttendeesLimit = null
         ) : ICommand<Result<Guid>>;
}
