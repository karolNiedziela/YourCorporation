using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record EventLimits
    {
        public const int MinimumNumberOfAttendes = 10;

        public int? AttendeesLimit { get; }

        public EventLimits(int? attendeesLimit = null)
        {
            if (attendeesLimit.HasValue && attendeesLimit.Value < MinimumNumberOfAttendes)
            {
                throw new CustomValidationException(ErrorCodes.Events.EventLimitsError);
            }

            AttendeesLimit = attendeesLimit;
        }
    }
}
