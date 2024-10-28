using Microsoft.AspNetCore.Http;
using Serilog.Context;
using YourCorporation.Shared.Abstractions.Extensions;

namespace YourCorporation.Shared.Infrastructure.Middlewares
{
    internal class RequestContextLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            Guid correlationId = context.GetCorrelationId();

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                return _next.Invoke(context);
            }
        }        
    }
}
