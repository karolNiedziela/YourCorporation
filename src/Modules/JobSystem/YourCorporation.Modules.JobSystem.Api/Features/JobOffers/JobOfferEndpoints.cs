using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.JobSystem.Api.Features.JobOffers.CreateJobOffer;
using YourCorporation.Modules.JobSystem.Api.Features.JobOffers.PublishJobOffer;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.JobSystem.Api.Features.JobOffers
{
    internal class JobOfferEndpoints : IMinimalApiEndpointDefinition
    {
        public const string GetJobOffer = nameof(GetJobOffer);
        public const string CreateJobOffer = nameof(CreateJobOffer);
        public const string PublishJobOffer = nameof(PublishJobOffer);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(JobOffersModule.BasePath + "/joboffers");

            group.MapGet("{jobOfferId:guid}", GetAsync)
             .WithName(GetJobOffer)
              .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get job offer"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", CreateJobOfferAsync)
                .WithName(CreateJobOffer)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Create job offer"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            group.MapPatch("{jobOfferId:guid}/publish", PublishJobOfferAsync)
                .WithName(PublishJobOffer)
                .WithMetadata(
                  new SwaggerOperationAttribute(summary: "Publish job offer"),
                  new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent),
                  new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid jobOfferId)
        {
            return TypedResults.Ok();
        }

        public static async Task<Results<Created, BadRequest<List<Error>>>> CreateJobOfferAsync(
            CreateJobOfferCommand command,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext context)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>>>(
                onSuccess: (Guid jobOfferId) =>
                {
                    var jobOfferLink = linkGenerator.GetUriByName(context,
                        endpointName: GetJobOffer,
                        values: new { jobOfferId = result.Value });

                    return TypedResults.Created(jobOfferLink);
                },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }

        public static async Task<Results<NoContent, BadRequest<List<Error>>>> PublishJobOfferAsync(
            [FromRoute] Guid jobOfferId,
            ISender sender)
        {
            var result = await sender.Send(new PublishJobOfferCommand(jobOfferId));

            return result.Match<Results<NoContent, BadRequest<List<Error>>>>(
                onSuccess: () => TypedResults.NoContent(),
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }
    }
}
