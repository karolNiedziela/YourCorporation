using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core
{
    internal static class ErrorCodes
    {
        public static class Events
        {
            public static Error BegginingAndEndOfEventError => new("Events.StartTimeOfEventEqualOrGreaterThanEndTime", "Start time of event cannot be later than or equal end time of event.");

            public static Error EventLimitsError => new("Events.MinimumAttendeesLimit", $"Minimum attendees limit must be higher or equal to {EventLimits.MinimumNumberOfAttendes}.");

            public static Error EmptyEventNameError => new("Events.EmptyEventName", $"Event name cannot be empty.");

            public static string EmptyEventDescriptionErrorCode = "Events.EmptyEventDescription";

            public static string MaxLengthEventDescriptionErrorCode = "Events.MaxLengthEventDescription";

            public static Error SpeakerAlreadyAdded => new Error("Events.SpeakerAlreadyAdded", "Speaker already added.");
        }

        public static class Sessions
        {
            public static Error EndTimeEarlierOrEqualStartTimeError = new("Sessions.EndTimeEarlierOrEqualStartTime", "Start time of session must be greater than end time of session.");

            public static Error NotInScopeOfEventError => new("Sessions.NotInScopeOfEvent", "Session start time and end session must be in event time range.");

            public static string SessionNameErrorCode = "SessionName";
        }

        public static class Shared
        {
            public static Error EmailError = new ("Email", "Invalid email format.");
        }
    }
}
