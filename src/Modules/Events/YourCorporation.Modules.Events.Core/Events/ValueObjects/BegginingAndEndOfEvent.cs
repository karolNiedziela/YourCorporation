using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record BegginingAndEndOfEvent
    {
        public DateTimeOffset StartTime { get; }

        public DateTimeOffset EndTime { get; }

        private BegginingAndEndOfEvent() { }

        public BegginingAndEndOfEvent(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if (startTime >= endTime)
            {
                throw new CustomValidationException(ErrorCodes.Events.BegginingAndEndOfEventError);
            }

            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
