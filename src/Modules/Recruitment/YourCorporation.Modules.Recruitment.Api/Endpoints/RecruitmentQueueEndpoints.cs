using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Modules.Recruitment.Application.Features.RecruitmentQueues.AddRecruitmentQueue;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Api.Endpoints
{
    internal class RecruitmentQueueEndpoints : IMinimalApiEndpointDefinition
    {
        public const string GetRecruitmentQueue = nameof(GetRecruitmentQueue);
        public const string AddRecruitmentQueue = nameof(AddRecruitmentQueue);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(RecruitmentModule.BasePath + "/recruitmentqueues");

            group.MapGet("{recruitmentQueueId:guid}", GetAsync)
             .WithName(GetRecruitmentQueue)
              .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Get recruitment queue"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", AddRecruitmentQueueAsync)
                .WithName(AddRecruitmentQueue)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Add recruitment queue"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest),
                    new ProducesResponseTypeAttribute(StatusCodes.Status409Conflict));

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid recruitmentQueueId, ISender sender, HttpContext context)
        {
            //var eventDto = await sender.Send(new GetDraftEventQuery(draftEventId));
            //if (draftEventDto is null)
            //{
            //    return TypedResults.NotFound();
            //}

            return TypedResults.Ok();
        }

        public static async Task<Results<Created, BadRequest<List<Error>>, Conflict<Error>>> AddRecruitmentQueueAsync(
            AddRecruitmentQueueCommand command,
            ISender sender,
            LinkGenerator linkGenerator,
            HttpContext context)
        {
            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>, Conflict<Error>>>(
                 onSuccess: (Guid recruitmentQueueId) =>
                {
                    var eventLink = linkGenerator.GetUriByName(context,
                          endpointName: GetRecruitmentQueue,
                          values: new { recruitmentQueueId = result.Value }
                          );

                    return TypedResults.Created(eventLink);
                },
                onError: (List<Error> errors) => TypedResults.BadRequest(errors),
                onConflict: (Error error) => TypedResults.Conflict(error));
        }
    }
}
