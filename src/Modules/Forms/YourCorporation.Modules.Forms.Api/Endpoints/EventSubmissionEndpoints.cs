using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Shared.Abstractions.MinimalApis;

namespace YourCorporation.Modules.Forms.Api.Endpoints
{
    internal class EventSubmissionEndpoints : IMinimalApiEndpointDefinition
    {
        public const string Submit = nameof(Submit);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            //var group = builder.MapGroup(FormsModule.BasePath + "/eventsubmissions");

            //group.MapPost("", () => { })
            //    .WithName(Submit)
            //     .WithMetadata(
            //        new SwaggerOperationAttribute(summary: "Submit event"),
            //        new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
            //        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            return builder;
        }

        public static async Task<Results<Ok, BadRequest>> SubmitEventSubmission()
        {
            return TypedResults.Ok();
        }
    }
}
