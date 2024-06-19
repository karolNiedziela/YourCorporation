using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using YourCorporation.Modules.Users.Api.Features.Users.CreateUser;
using YourCorporation.Shared.Abstractions.MinimalApis;

namespace YourCorporation.Modules.Users.Api.Features.Users
{
    internal class UserEndpoints : IMinimalApiEndpointDefinition
    {
        public const string CreateUser = nameof(CreateUser);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(UsersModule.BasePath + "/users");

            group.MapPost("", CreateUserAsync)
                .WithName(CreateUser)
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Create user"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status503ServiceUnavailable));

            return builder;
        }

        public static async Task<Results<Ok, StatusCodeHttpResult>> CreateUserAsync(CreateUserCommand command, ISender sender)
        {
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return TypedResults.StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }

            return TypedResults.Ok();
        }
    }
}
