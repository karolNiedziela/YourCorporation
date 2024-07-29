using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.Users.Api.Authorization;
using YourCorporation.Modules.Users.Api.Features.Models;
using YourCorporation.Modules.Users.Api.Features.Users.AssignUserRoleWebhook;
using YourCorporation.Modules.Users.Api.Features.Users.CreateUserWebhookHandler;
using YourCorporation.Modules.Users.Api.Features.Users.Models;
using YourCorporation.Shared.Abstractions.MinimalApis;

namespace YourCorporation.Modules.Users.Api.Features.Users
{
    internal class UserEndpoints : IMinimalApiEndpointDefinition
    {
        public const string CreateUser = nameof(CreateUser);
        public const string AssignRole = nameof(AssignRole);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(UsersModule.BasePath + "/users");

            group.MapPost("", CreateUserAsync)
                .WithName(CreateUser)
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Create user"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .AddEndpointFilter<SupabaseSignatureEndpointFilter>();

            group.MapPost("assign-role", AssignRoleAsync)
                .WithName(AssignRole)
                .WithMetadata(
                 new SwaggerOperationAttribute(summary: "Assign role"),
                 new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                 new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest))
                .AddEndpointFilter<SupabaseSignatureEndpointFilter>();

            return builder;
        }      

        public static async Task<Results<Ok, BadRequest>> CreateUserAsync(InsertPayload<UserSupabaseModel> request, ISender sender)
        {
            var result = await sender.Send(new CreateUserWebhookComand(request));

            return result.IsFailure ? TypedResults.BadRequest() : TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> AssignRoleAsync(InsertPayload<UserRoleSupabaseModel> request, ISender sender)
        {
            var result = await sender.Send(new AssignUserRoleWebhookCommand(request));

            return result.IsFailure ? TypedResults.BadRequest() : TypedResults.Ok();
        }
    }
}
