using System.ComponentModel.DataAnnotations;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects
{
    internal record PrivatePhone
    {
        public string Value { get; } = default!;

        private PrivatePhone() { }

        private PrivatePhone(string value)
        {
            Value = value;
        }

        public static Result<PrivatePhone> Create(string value)
        {
            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(value))
            {
                return ErrorCodes.Contacts.InvalidPrivatePhoneFormatError;
            }

            return new PrivatePhone(value);
        }
    }
}
