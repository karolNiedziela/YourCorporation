namespace YourCorporation.Shared.Abstractions.Exceptions
{
    public class CustomValidationException : YourCorporationException
    {
        public CustomValidationException(Error error) : base(error)
        {
        }
    }
}
