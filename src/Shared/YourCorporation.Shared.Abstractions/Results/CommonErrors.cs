namespace YourCorporation.Shared.Abstractions.Results
{
    public static class CommonErrors
    {
        public static Error Empty(string errorCode, string humanReadeablePropertyName)
            => Error.Validation(errorCode, $"{humanReadeablePropertyName} cannot be empty.");

        public static Error MaxLength(string errorCode, int maxLength, string humanReadeablePropertyName)
            => Error.Validation(errorCode, $"{humanReadeablePropertyName} cannot be longer than {maxLength}.");
    }
}
