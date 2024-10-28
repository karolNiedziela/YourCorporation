using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace YourCorporation.Shared.Abstractions.Extensions
{
    public static class HttpContextExtensions
    {
        public const string CorrelationIdHeaderName = "X-Correlation-Id";

        public static Guid GetCorrelationId(this HttpContext context)
        {
            context.Request.Headers.TryGetValue(
                CorrelationIdHeaderName, out StringValues correlationId);

            if (Guid.TryParse(correlationId.FirstOrDefault(), out var parsedCorrelationId))
            {
                return parsedCorrelationId;
            }

            return Guid.NewGuid();
        }
    }
}
