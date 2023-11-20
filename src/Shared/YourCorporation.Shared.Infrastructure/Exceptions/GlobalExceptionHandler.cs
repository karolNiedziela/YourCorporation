using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net;
using YourCorporation.Shared.Abstractions.Exceptions;
using Microsoft.Extensions.Hosting;

namespace YourCorporation.Shared.Infrastructure.Exceptions
{
    internal sealed class GlobalExceptionHandler : IMiddleware
    {
        private readonly IExceptionToErrorResponseMapper _exceptionToErrorResponseMapper;

        public GlobalExceptionHandler(IExceptionToErrorResponseMapper exceptionToErrorResponseMapper)
        {
            _exceptionToErrorResponseMapper = exceptionToErrorResponseMapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {               
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var errorResponse = _exceptionToErrorResponseMapper.Map(exception);
            if (errorResponse is null)
            {
                return;
            }

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

            var problem = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                    {
                        Title = errorResponse!.ErrorCode,
                        Detail = exception.Message,
                        Status = (int)errorResponse.StatusCode,
                    }
            };

            problem.ProblemDetails.Extensions.Add("errorCode", errorResponse.ErrorCode);
            problem.ProblemDetails.Extensions.Add("errors", errorResponse.Errors);
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            problem.ProblemDetails.Extensions.Add("traceId", traceId);

            var webHostEnvironment = httpContext!.RequestServices.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsDevelopment())
            {
                problem.ProblemDetails.Extensions.Add("stackTrace", exception.StackTrace);
            }

            var problemDetailsService = httpContext!.RequestServices.GetRequiredService<IProblemDetailsService>();
            await problemDetailsService.WriteAsync(problem);
        }
    }
}
