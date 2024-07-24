using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace YourCorporation.Shared.Infrastructure.Auth
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var systemUserId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

            if (!Guid.TryParse(systemUserId, out var parsedSystemUserId))
            {

            }

            return Task.CompletedTask;
        }
    }
}
