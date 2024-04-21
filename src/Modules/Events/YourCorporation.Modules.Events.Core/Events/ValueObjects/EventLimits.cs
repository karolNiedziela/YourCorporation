using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record EventLimits
    {
        public static int MinimumNumberOfAttendes = 10;

        public int? AttendeesLimit { get; }

        private EventLimits(int? attendeesLimit = null)
        {           
            AttendeesLimit = attendeesLimit;
        }

        public static Result<EventLimits> Create(int? attendeesLimit = null)
        {
            if (attendeesLimit.HasValue && attendeesLimit.Value < MinimumNumberOfAttendes)
            {
                return ErrorCodes.Events.EventLimitsError;
            }

            return new EventLimits(attendeesLimit);
        }
    }
}
