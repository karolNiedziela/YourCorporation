using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal record BegginingAndEndOfSession
    {
        public DateTimeOffset StartTime { get; }

        public DateTimeOffset EndTime { get; }

        private BegginingAndEndOfSession() { }

        public BegginingAndEndOfSession(
            DateTimeOffset startTimeSession,
            DateTimeOffset endTimeSession,
            BegginingAndEndOfEvent begginingAndEndOfEvent)
        {
            if (startTimeSession >= endTimeSession)
            {
                throw new InvalidOperationException();
            }

            var isSessionOverlapsEvent = startTimeSession < begginingAndEndOfEvent.EndTime && begginingAndEndOfEvent.StartTime < endTimeSession;
            if (!isSessionOverlapsEvent)
            {                
                throw new CustomValidationException(ErrorCodes.Sessions.BegginingAndEndOfSessionError);
            }

            StartTime = startTimeSession;
            EndTime = endTimeSession;
        }
    }
}
