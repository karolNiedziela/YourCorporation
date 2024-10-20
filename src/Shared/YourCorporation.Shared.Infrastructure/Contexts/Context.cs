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

        public IIdentityContext Identity { get; }

        public Context() : this(Guid.NewGuid(), $"{Guid.NewGuid():N}")
        {
        }

        public Context(HttpContext httpContext) : this(httpContext.GetCorrelationId(), httpContext.TraceIdentifier, new IdentityContext(httpContext.User))
        {
        }

        public Context(Guid correlationId, string traceId, IIdentityContext identity = null)
        {
            CorrelationId = correlationId;
            TraceId = traceId;
            Identity = identity ?? IdentityContext.Empty;
        }

        public Context(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public static IContext Empty => new Context();
    }
}
