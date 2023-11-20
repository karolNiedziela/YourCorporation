using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Modules.Events.Core
{
    internal static class ErrorCodes
    {
        public static class Events
        {
            public static Error BegginingAndEndOfEventError => new("StartTimeOfEventEqualOrGreaterThanEndTime", "Start time of event cannot be later than or equal end time of event.");

            public static Error EventLimitsError => new("MinimumAttendeesLimit", $"Minimum attendees limit must be higher or equal to {EventLimits.MinimumNumberOfAttendes}.");

            public static string EmptyEventNameError = "EmptyEventName";

            public static Error NotFoundEventError(Guid eventId) => new("NotFoundEventError", $"Event with id '{eventId}' was not found.");

            public static string EmptyEventDescriptionError = "EmptyEventDescription";


            public static string MaxLengthEventDescription = "MaxLengthEventDescription";
        }

        public static class Sessions
        {
            public static Error BegginingAndEndOfSessionError => new("BegginingAndEndOfSession", "Session start time and end session must be in event time range.");

            public static string SessionNameError = "SessionName";
        }

        public static class Shared
        {
            public static Error EmailError = new ("Email", "Invalid email format.");
        }
    }
}
