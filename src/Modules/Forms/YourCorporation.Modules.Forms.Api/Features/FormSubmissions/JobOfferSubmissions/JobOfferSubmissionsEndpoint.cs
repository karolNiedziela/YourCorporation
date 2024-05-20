using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.Forms.Api.Features.FormSubmissions.JobOfferSubmissions.SubmitJobOffer;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Forms.Api.Features.FormSubmissions.JobOfferSubmissions
{
    internal class JobOfferSubmissionsEndpoint : IMinimalApiEndpointDefinition
    {
        public const string GetJobOfferSubmission = nameof(GetJobOfferSubmission);
        public const string SubmitJobOffer = nameof(SubmitJobOffer);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(FormsModule.BasePath + "/joboffersubmissions");

            group.MapGet("{jobOfferSubmissionId:guid}", GetAsync)
                .WithName(GetJobOfferSubmission)
                .WithMetadata(
                   new SwaggerOperationAttribute(summary: "Get job offer submission"),
                   new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                   new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", SubmitJobOfferAsync)
                .Accepts<SubmitJobOfferCommand>("multipart/form-data")
                .WithName(SubmitJobOffer)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add job offer submission"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .DisableAntiforgery();

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid workLocationId)
        {
            return TypedResults.Ok();
        }

        public static async Task<Results<Created, BadRequest<List<Error>>>> SubmitJobOfferAsync(
            [FromForm] SubmitJobOfferCommand command,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext httpContext)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>>>(
                onSuccess: (Guid jobOfferSubmissionId) =>
                {
                    var jobOfferSubmissionLink = linkGenerator.GetUriByName(httpContext,
                        endpointName: GetJobOfferSubmission,
                        values: new { jobOfferSubmissionId });

                    return TypedResults.Created(jobOfferSubmissionLink);
                },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors));
        }
    }
}
