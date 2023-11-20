namespace YourCorporation.Shared.Abstractions.Exceptions
{
    public abstract class YourCorporationException : Exception
    {
        public string ErrorCode { get; }

        protected YourCorporationException(Error error) : base(error.Message) 
        {
            ErrorCode = error.ErrorCode;
        }
    }
}
