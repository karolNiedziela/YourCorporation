using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Domain
{
    internal static class ErrorCodes
    {
        public static class JobOffers
        {
            public static Error InvalidStatusError(string status) => new ("JobOffers.InvalidStatus", $"Error during changing job offer status to '{status}'.");
        }

        public static class WorkLocations
        {
            public static Error InvalidCodeError => new("WorkLocations.InvalidCode", "Invalid country code format, example of valid: PL_LBN.");
        }
    }
}
