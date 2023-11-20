using System.ComponentModel.DataAnnotations;
using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Modules.Events.Core.Shared.ValueObjects
{
    internal record Email
    {
        public string Value { get; } = default!;

        private Email() { }

        public Email(string value)
        {
            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(value))
            {
                throw new CustomValidationException(ErrorCodes.Shared.EmailError);
            }

            Value = value;
        }
    }
}
