﻿using YourCorporation.Modules.Events.Core.Events.Enums;
using YourCorporation.Shared.Abstractions.Commands;

namespace YourCorporation.Modules.Events.Application.Commands.Events.CreateEvent
{
    internal record CreateEventCommand(
         string Name,
         EventCategory Category,
         EventMode Mode,
         DateTimeOffset StartTime,
         DateTimeOffset EndTime,
         int? AttendeesLimit = null
         ) : ICommand<Guid>;
}
