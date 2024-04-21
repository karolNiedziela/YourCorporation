using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Shared.Abstractions.ValueObjects
{
    public record FirstName
    {
        public const int MaxLength = 100;

        public string Value { get; } = default!;

        private FirstName() { }

        private FirstName(string value)
        {            
            Value = value;
        }

        public static Result<FirstName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return CommonErrors.Empty(SharedErrorCodes.EmptyFirstNameErrorCode, "FirstName");
            }

            if (value.Length > MaxLength)
            {
                return CommonErrors.MaxLength(SharedErrorCodes.MaxLengthFirstNameErrorCode, MaxLength, "FirstName");
            }

            return new FirstName(value);
        }
    }
}
