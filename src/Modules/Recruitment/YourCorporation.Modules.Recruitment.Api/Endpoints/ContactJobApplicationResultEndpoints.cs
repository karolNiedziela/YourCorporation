using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.Recruitment.Application.Features.ContactJobApplicationResults.CreateContactJobApplicationResult;
using YourCorporation.Modules.Recruitment.Application.Features.RecruitmentQueues.AddRecruitmentQueue;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Api.Endpoints
{
    internal class ContactJobApplicationResultEndpoints : IMinimalApiEndpointDefinition
    {
        public const string GetContactJobApplicationResult = nameof(GetContactJobApplicationResult);
        public const string CreateContactJobApplicationResult = nameof(CreateContactJobApplicationResult);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(RecruitmentModule.BasePath + "/contactjobapplicationresults");

            group.MapGet("{contactJobApplicationResultId:guid}", GetAsync)
            .WithName(GetContactJobApplicationResult)
             .WithMetadata(
                new SwaggerOperationAttribute(summary: "Get contact job application result"),
                new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", CreateContactJobApplicationResultAsync)
                .WithName(CreateContactJobApplicationResult)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Create contact job application result"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest),
                    new ProducesResponseTypeAttribute(StatusCodes.Status409Conflict));


            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid contactJobApplicationResultId, ISender sender, HttpContext context)
        {
            return TypedResults.Ok();
        }

        public static async Task<Results<Created, BadRequest<List<Error>>, Conflict<Error>>> CreateContactJobApplicationResultAsync(
           CreateContactJobApplicationResultCommand command,
           ISender sender,
           LinkGenerator linkGenerator,
           HttpContext context)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>, Conflict<Error>>>(
                 onSuccess: (Guid recruitmentQueueId) =>
                 {
                     var contactJobApplicationResultLink = linkGenerator.GetUriByName(context,
                           endpointName: GetContactJobApplicationResult,
                           values: new { contactJobApplicationResultId = result.Value }
                           );

                     return TypedResults.Created(contactJobApplicationResultLink);
                 },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors),
                onConflict: (Error error) => TypedResults.Conflict(error));
        }
    }
}
