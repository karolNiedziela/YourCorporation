using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.JobSystem.Api.Features.WorkLocations.AddWorkLocation;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.WorkLocations
{
    internal sealed class WorkLocationEndpoints : IMinimalApiEndpointDefinition
    {
        public const string GetWorkLocation = nameof(GetWorkLocation);
        public const string AddWorkLocation = nameof(AddWorkLocation);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(JobOffersModule.BasePath + "/worklocations");

            group.MapGet("{workLocationId:guid}", GetAsync)
            .WithName(GetWorkLocation)
            .WithMetadata(
               new SwaggerOperationAttribute(summary: "Get work location"),
               new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
               new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", AddWorkLocationAsync)
                .WithName(AddWorkLocation)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add work location"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest),
                    new ProducesResponseTypeAttribute(StatusCodes.Status409Conflict));

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid workLocationId)
        {
            return TypedResults.Ok();
        }

        public static async Task<Results<Created, BadRequest<List<Error>>, Conflict<Error>>> AddWorkLocationAsync(
           AddWorkLocationCommand command,
           ISender sender,
           LinkGenerator linkGenerator,
           HttpContext httpContext)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>, Conflict<Error>>>(
                 onSuccess: (Guid workLocationId) =>
                 {
                     var workLocationLink = linkGenerator.GetUriByName(httpContext,
                         endpointName: GetWorkLocation,
                         values: new { workLocationId });

                     return TypedResults.Created(workLocationLink);
                 },
                 onError: (List<Error> errors) => TypedResults.BadRequest(errors),
                 onConflict: (Error error) => TypedResults.Conflict(error));
        }
    }
}
