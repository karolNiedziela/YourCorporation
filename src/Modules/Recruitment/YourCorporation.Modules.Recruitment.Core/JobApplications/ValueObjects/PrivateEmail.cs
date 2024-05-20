using System.ComponentModel.DataAnnotations;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal record PrivateEmail
    {
        public string Value { get; } = default!;

        private PrivateEmail() { }

        private PrivateEmail(string value)
        {
            Value = value;
        }

        public static Result<PrivateEmail> Create(string value)
        {
            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(value))
            {
                return ErrorCodes.Candidates.InvalidPrivateEmailFormatError;
            }

            return new PrivateEmail(value);
        }
    }
}
