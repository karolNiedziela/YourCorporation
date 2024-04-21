namespace YourCorporation.Shared.Abstractions.Results
{
    public sealed record Error(string Code, string Message = null, ErrorType ErrorType = ErrorType.Validation)
    {
        public static readonly Error None = new(string.Empty);

        public static Error Validation(string errorCode, string message = null) 
            => new(errorCode, message, ErrorType.Validation);

        public static Error NotFound(string errorCode, string message = null)
            => new Error(errorCode, message, ErrorType.NotFound);

    }
}
