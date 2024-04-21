using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record BegginingAndEndOfEvent
    {
        public DateTimeOffset StartTime { get; }

        public DateTimeOffset EndTime { get; }

        private BegginingAndEndOfEvent() { }

        private BegginingAndEndOfEvent(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public static Result<BegginingAndEndOfEvent> Create(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if (startTime >= endTime)
            {
                return ErrorCodes.Events.BegginingAndEndOfEventError;
            }

            return new BegginingAndEndOfEvent(startTime, endTime);
        }
    }
}
