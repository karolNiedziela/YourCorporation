namespace YourCorporation.Shared.Abstractions.Exceptions.Common
{
    public sealed class MaxLengthException : CustomValidationException
    {
        public MaxLengthException(string errorCode, int maxLength) 
            : base(new Error(errorCode, $"Value cannot be longer than {maxLength}."))
        {
        }
    }
}
