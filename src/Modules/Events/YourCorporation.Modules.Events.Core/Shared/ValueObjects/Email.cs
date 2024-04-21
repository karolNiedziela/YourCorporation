using System.ComponentModel.DataAnnotations;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Shared.ValueObjects
{
    internal record Email
    {
        public string Value { get; } = default!;

        private Email() { }

        private Email(string value)
        {          
            Value = value;
        }

        public static Result<Email> Create(string value)
        {
            var emailAddressAttribute = new EmailAddressAttribute();
            if (!emailAddressAttribute.IsValid(value))
            {
                return ErrorCodes.Shared.EmailError;
            }

            return new Email(value);
        }
    }
}
