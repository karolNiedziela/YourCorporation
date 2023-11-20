using YourCorporation.Shared.Abstractions.Exceptions;
using YourCorporation.Shared.Abstractions.Exceptions.Common;

namespace YourCorporation.Shared.Abstractions.ValueObjects
{
    public record FirstName
    {
        public const int MaxLength = 100;

        public string Value { get; } = default!;

        private FirstName() { }

        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyValueException(SharedErrorCodes.EmptyFirstName);
            }

            if (value.Length > MaxLength)
            {
                throw new MaxLengthException(SharedErrorCodes.MaxLengthFirstName, MaxLength);
            }

            Value = value;
        }
    }
}
