using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal record BegginingAndEndOfSession
    {
        public DateTimeOffset StartTime { get; }

        public DateTimeOffset EndTime { get; }

        private BegginingAndEndOfSession() { }

        private BegginingAndEndOfSession(
            DateTimeOffset startTimeSession,
            DateTimeOffset endTimeSession)
        {
            StartTime = startTimeSession;
            EndTime = endTimeSession;
        }

        public static Result<BegginingAndEndOfSession> Create(DateTimeOffset startTimeSession,
            DateTimeOffset endTimeSession,
            BegginingAndEndOfEvent begginingAndEndOfEvent)
        {
            if (startTimeSession >= endTimeSession)
            {
                return ErrorCodes.Sessions.EndTimeEarlierOrEqualStartTimeError;
            }

            var isSessionOverlapsEvent = startTimeSession < begginingAndEndOfEvent.EndTime && begginingAndEndOfEvent.StartTime < endTimeSession;
            if (!isSessionOverlapsEvent)
            {
                return ErrorCodes.Sessions.NotInScopeOfEventError;
            }

            return new BegginingAndEndOfSession(startTimeSession, endTimeSession);
        }
    }
}
