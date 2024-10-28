using System.Text.RegularExpressions;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal partial record LinkedinUrl
    {
        [GeneratedRegex(LinkedingPattern)]
        private static partial Regex IsLinkedin();

        public const string LinkedingPattern = @"www\.linkedin\.com";

        public string Value { get; } = default!;

        private LinkedinUrl() { }

        private LinkedinUrl(string value)
        {
            Value = value;
        }

        public static Result<LinkedinUrl> Create(string value)
        {
            if (!IsLinkedin().IsMatch(value))
            {
                return ErrorCodes.Contacts.InvalidLinkedinUrlError;
            }

            return new LinkedinUrl(value);
        }
    }
}
