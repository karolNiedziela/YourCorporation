using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using YourCorporation.Shared.Abstractions.Auth;

namespace YourCorporation.Shared.Infrastructure.Auth
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private const string _realmAccessClaim = "realm_access";
        private const string _userRoleClaim = "user_roles";
        private const string _systemAdministratorRole = "System Administrator";

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (IsSupabaseSystemAdministrator(context))
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

        private static bool IsKeyCloakSystemAdministrator(AuthorizationHandlerContext context)
        {
            if (!context.User.HasClaim(c => c.Type == _realmAccessClaim))
            {
                return false;
            }

            var realmRolesClaim = context.User.Claims.FirstOrDefault(c => c.Type == _realmAccessClaim)?.Value;
            var realRoles = JsonConvert.DeserializeObject<RealmAccess>(realmRolesClaim)?.Roles ?? [];

            return realRoles.Contains(_systemAdministratorRole);
        }

        private static bool IsSupabaseSystemAdministrator(AuthorizationHandlerContext context)
        {
            if (!context.User.HasClaim(c => c.Type == _userRoleClaim))
            {
                return false;
            }

            var userRolesClaimValue = context.User.Claims.FirstOrDefault(c => c.Type == _userRoleClaim)?.Value;
            var userRoles = userRolesClaimValue.Split(',');

            return userRoles.Any(x => x.Contains(_systemAdministratorRole));
        }

        private class RealmAccess
        {
            public string[] Roles { get; set; }
        }
    }
}
