using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using YourCorporation.Shared.Abstractions.Auth;
using YourCorporation.Shared.Abstractions.Contexts;

namespace YourCorporation.Shared.Infrastructure.Auth
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IContext _context;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory, IContext context)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (_context.Identity.IsSystemAdministrator())
            {
                context.Succeed(requirement);
                return;
            }

            var systemUserId = context.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (!Guid.TryParse(systemUserId, out var parsedSystemUserId))
            {
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = await permissionService.GetPermissionAsync(parsedSystemUserId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }       
    }
}
