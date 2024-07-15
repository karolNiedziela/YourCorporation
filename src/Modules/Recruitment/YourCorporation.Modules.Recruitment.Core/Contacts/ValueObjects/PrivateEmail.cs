using System.ComponentModel.DataAnnotations;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal record PrivateEmail
    {
        public string Value { get; } = default!;

        private PrivateEmail() { }

        private PrivateEmail(string value)
        {
            Value = value;
        }

        public static Result<PrivateEmail> Create(string value, params string[] invalidDomains)
        {
            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(value))
            {
                return ErrorCodes.Candidates.InvalidPrivateEmailFormatError;
            }

            var domain = value.Split("@").Last();
            if (invalidDomains.Contains(domain))
            {
                return ErrorCodes.Candidates.InvalidPrivateEmailDomainError;
            }

            return new PrivateEmail(value);
        }
    }
}
