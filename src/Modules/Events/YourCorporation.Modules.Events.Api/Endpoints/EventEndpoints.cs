using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.Events.Application.Commands.Events.CreateEvent;
using YourCorporation.Modules.Events.Application.Commands.Events.GoLive;
using YourCorporation.Shared.Abstractions.MinimalApis;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Api.Endpoints
{
    internal class EventEndpoints : IMinimalApiEndpointDefinition
    {
        public const string GetEvent = nameof(GetEvent);
        public const string CreateEvent = nameof(CreateEvent);
        public const string GoLive = nameof(GoLive);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(EventsModule.BasePath + "/events");

            group.MapGet("", () =>
            {
                return Results.Ok("Events");
            });

            group.MapGet("{eventId:guid}", GetAsync)
               .WithName(GetEvent)
                .WithMetadata(
                   new SwaggerOperationAttribute(summary: "Get event"),
                   new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                   new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", CreateEventAsync)
                .WithName(CreateEvent)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Create event"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            group.MapPost("{eventId}/golive", GoLiveAsync)
                .WithName(GoLive)
                .WithMetadata(
                    new SwaggerOperationAttribute(summary: "Go live"),
                    new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent),
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid eventId, ISender sender)
        {
            //var eventDto = await sender.Send(new GetDraftEventQuery(draftEventId));
            //if (draftEventDto is null)
            //{
            //    return TypedResults.NotFound();
            //}

            return TypedResults.Ok();
        }

        public static async Task<Results<Created, BadRequest<List<Error>>>> CreateEventAsync(CreateEventCommand command, ISender sender, LinkGenerator linkGenerator, HttpContext context)
        {


            var result = await sender.Send(command);

            return result.Match<Results<Created, BadRequest<List<Error>>>>(
                onSuccess: (Guid eventId) =>
                {
                    var eventLink = linkGenerator.GetUriByName(context,
                          endpointName: GetEvent,
                          values: new { eventId = result.Value }
                          );

                    return TypedResults.Created(eventLink);
                },
                onError: (errors) => TypedResults.BadRequest(errors));        
        }

        public static async Task<Results<NoContent, ValidationProblem>> GoLiveAsync(GoLiveCommand command, ISender sender)
        {
            await sender.Send(command);

            return TypedResults.NoContent();
        }
    }
}
