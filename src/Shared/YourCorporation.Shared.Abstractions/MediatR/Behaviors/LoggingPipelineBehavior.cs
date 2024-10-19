using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Shared.Abstractions.MediatR.Behaviors
{
    internal class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResult
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var moduleName = GetModuleName(typeof(TRequest));

            _logger.LogInformation(
              "[{ModuleName}] Starting request {@RequestName}, {@DateTimeUtc}",
              moduleName,
              typeof(TRequest).Name,
              DateTime.UtcNow);

            var result = await next();

            if (result.IsFailure)
            {
                _logger.LogError(
                     "[{ModuleName}] Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                     moduleName,
                    typeof(TRequest).Name,
                    result.Errors,
                    DateTime.UtcNow);
            }

            _logger.LogInformation(
                 "[{ModuleName}] Completed request {@RequestName}, {@DateTimeUtc}",
                 moduleName,
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return result;
        }

        public string GetModuleName(Type type, string namespacePart = "Modules", int splitIndex = 2)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.Contains(namespacePart)
                ? type.Namespace.Split(".")[splitIndex]
                : string.Empty;
        }
    }
}
