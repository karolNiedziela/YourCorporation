using System.Text.RegularExpressions;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Domain.WorkLocations
{
    internal partial record WorkLocationCode
    {
        [GeneratedRegex(CodePattern)]
        private static partial Regex CodeRegex();

        public const string CodePattern = @"^[A-Z]{2}_[A-Z]{3}$";

        public string Value { get; private set; }

        private WorkLocationCode() { }

        private WorkLocationCode(string value)
        {
            Value = value;
        }

        public static Result<WorkLocationCode> Create(string value)
        {
            if (!CodeRegex().IsMatch(value))
            {
                return ErrorCodes.WorkLocations.InvalidCodeError;
            }

            return new WorkLocationCode(value);
        }

        public static implicit operator string(WorkLocationCode code) => code.Value;
    }
}
