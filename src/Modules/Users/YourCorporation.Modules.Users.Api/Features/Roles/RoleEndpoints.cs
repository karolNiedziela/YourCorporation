using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Swashbuckle.AspNetCore.Annotations;
using YourCorporation.Modules.Users.Api.Features.Models;
using YourCorporation.Modules.Users.Api.Features.Roles.CreateRoleWebhookHandler;
using YourCorporation.Modules.Users.Api.Features.Users.Models;
using YourCorporation.Shared.Abstractions.MinimalApis;

namespace YourCorporation.Modules.Users.Api.Features.Roles
{
    internal class RoleEndpoints : IMinimalApiEndpointDefinition
    {
        public const string GetRole = nameof(GetRole);
        public const string CreateRole = nameof(CreateRole);

        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup(UsersModule.BasePath + "/roles");

            group.MapGet("{roleId:guid}", GetAsync)
             .WithName(GetRole)
             .WithMetadata(
                new SwaggerOperationAttribute(summary: "Get role"),
                new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
                new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));

            group.MapPost("", CreateRoleAsync)
              .WithName(CreateRole)
              .WithMetadata(
               new SwaggerOperationAttribute(summary: "Create role"),
               new ProducesResponseTypeAttribute(StatusCodes.Status200OK),
               new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));

            return builder;
        }

        public static async Task<Results<Ok, NotFound>> GetAsync(Guid roleId, ISender sender, HttpContext context)
        {
            return TypedResults.Ok();
        }

        public static async Task<Results<Ok, BadRequest>> CreateRoleAsync(InsertPayload<RoleSupabaseModel> request, ISender sender)
        {
            var result = await sender.Send(new CreateRoleWebhookCommand(request));

            return result.IsFailure ? TypedResults.BadRequest() : TypedResults.Ok();
        }
    }
}
