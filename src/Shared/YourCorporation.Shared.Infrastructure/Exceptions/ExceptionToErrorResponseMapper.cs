using System.Net;
using YourCorporation.Shared.Abstractions.Behaviors;
using YourCorporation.Shared.Abstractions.Exceptions;

namespace YourCorporation.Shared.Infrastructure.Exceptions
{
    internal sealed class ExceptionToErrorResponseMapper : IExceptionToErrorResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
             => exception switch
             {
                 YourCorporationException yourCorporationException => new ExceptionResponse(
                     yourCorporationException.ErrorCode,
                     GetValidationError(yourCorporationException),
                     HttpStatusCode.BadRequest),

                 ValidationException validationException => new ExceptionResponse(
                     "Validation",
                     validationException.Errors.ToArray(),
                     HttpStatusCode.BadRequest),

                 _ => new ExceptionResponse(
                         "UnexpectedError",
                         GetInternalServerError(),
                         HttpStatusCode.InternalServerError)
             };

        private static ValidationError[] GetValidationError(YourCorporationException yourCorporationException)
            => new ValidationError[]
            {
                new()
                {
                    Message = yourCorporationException.Message,
                }
            };

        private static ValidationError[] GetInternalServerError()
            => new ValidationError[]
            {
                new ()
                {
                     Message = "An unexpected error occurred"
                }
            };
    }
}
