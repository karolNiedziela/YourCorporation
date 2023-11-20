using System.Net;
using YourCorporation.Shared.Abstractions.Behaviors;

namespace YourCorporation.Shared.Abstractions.Exceptions
{
    public record ExceptionResponse(string ErrorCode, ValidationError[] Errors, HttpStatusCode StatusCode);
}
