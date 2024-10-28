using System.Data;
using System.Security.Claims;
using YourCorporation.Shared.Abstractions.Contexts;

namespace YourCorporation.Shared.Infrastructure.Contexts
{
    internal class IdentityContext : IIdentityContext
    {
        private const string _userRolesClaim = "user_roles";
        private const string _systemAdministratorRole = "System Administrator";

        public bool IsAuthenticated { get; }

        public Guid Id { get; }

        public string[] Roles { get; } = [];

        public Dictionary<string, IEnumerable<string>> Claims { get; } = [];

        private IdentityContext() { }

        public IdentityContext(Guid? id)
        {
            Id = id ?? Guid.Empty;
            IsAuthenticated = id.HasValue;
        }

        public IdentityContext(ClaimsPrincipal principal)
        {
            var id = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (principal?.Identity is null || id is null)
            {
                return;
            }

            IsAuthenticated = principal.Identity?.IsAuthenticated is true;
            Id = IsAuthenticated ? Guid.Parse(id) : Guid.Empty;
            Roles = principal.Claims.FirstOrDefault(x => x.Type == _userRolesClaim)?.Value.Split(",");
            Claims = principal.Claims.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
        }

        public bool IsSystemAdministrator() => Roles.Any(x => x.Contains(_systemAdministratorRole));

        public static IIdentityContext Empty => new IdentityContext();
    }
}
