using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Shared.Abstractions.ValueObjects
{
    public record LastName
    {
        public const int MaxLength = 100;

        public string Value { get; } = default!;

        private LastName() { }

        private LastName(string value)
        {            
            Value = value;
        }

        public static Result<LastName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return CommonErrors.Empty(SharedErrorCodes.EmptyLastNameErrorCode, "LastName");
            }

            if (value.Length > MaxLength)
            {
                return CommonErrors.MaxLength(SharedErrorCodes.MaxLengthLastNameErrorCode, MaxLength, "LastName");
            }

            return new LastName(value);
        }
    }
}
