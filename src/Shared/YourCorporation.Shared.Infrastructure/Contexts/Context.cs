using Microsoft.AspNetCore.Http;
using YourCorporation.Shared.Abstractions.Contexts;
using YourCorporation.Shared.Abstractions.Extensions;

namespace YourCorporation.Shared.Infrastructure.Contexts
{
    internal class Context : IContext
    {
        public Guid RequestId { get; } = Guid.NewGuid();

        public Guid CorrelationId { get; }

        public string TraceId { get; }

        public Context() : this(Guid.NewGuid(), $"{Guid.NewGuid():N}")
        {
        }

        public Context(HttpContext httpContext) : this(httpContext.GetCorrelationId(), httpContext.TraceIdentifier)
        {
        }

        public Context(Guid correlationId, string traceId)
        {
            CorrelationId = correlationId;
            TraceId = traceId;
        }

        public Context(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public static IContext Empty => new Context();
    }
}
