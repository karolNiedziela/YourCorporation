namespace YourCorporation.Shared.Abstractions.Exceptions
{
    public interface IExceptionToErrorResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
