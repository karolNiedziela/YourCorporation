namespace YourCorporation.Shared.Abstractions.Exceptions.Common
{
    public sealed class EmptyValueException : CustomValidationException
    {
        public EmptyValueException(string errorCode) : base(new Error(errorCode, "Value cannot be empty."))
        {
        }
    }
}
