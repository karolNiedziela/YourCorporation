using YourCorporation.Shared.Abstractions.Exceptions.Common;

namespace YourCorporation.Shared.Abstractions.ValueObjects
{
    public record LastName
    {
        public const int MaxLength = 100;

        public string Value { get; } = default!;

        private LastName() { }

        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyValueException(SharedErrorCodes.EmptyLastName);
            }

            if (value.Length > MaxLength)
            {
                throw new MaxLengthException(SharedErrorCodes.MaxLengthLastName, MaxLength);
            }

            Value = value;
        }
    }
}
