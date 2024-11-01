using System.Data;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using YourCorporation.Shared.Abstractions.Contexts;

namespace YourCorporation.Shared.Infrastructure.Contexts
{
    internal class IdentityContext : IIdentityContext
    {
        private const string _userRolesClaim = "user_roles";
        private const string _systemAdministratorRole = "System Administrator";

        public bool IsAuthenticated { get; }

        public Guid Id { get; }

        public string FullName { get; }

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
            FullName = TryGetFullName(principal);
            Roles = principal.Claims.FirstOrDefault(x => x.Type == _userRolesClaim)?.Value.Split(",");
            Claims = principal.Claims.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
        }

        public bool IsSystemAdministrator() => Roles.Any(x => x.Contains(_systemAdministratorRole));

        public static IIdentityContext Empty => new IdentityContext();

        private string TryGetFullName(ClaimsPrincipal principal)
        {
            var userMetadataClaim = principal.FindFirst("user_metadata");
            if (userMetadataClaim == null)
            {
                return string.Empty;
            }

            var metadata = JsonSerializer.Deserialize<UserMetadata>(userMetadataClaim.Value);
            var firstName = metadata?.FirstName ?? string.Empty;
            var lastName = metadata?.LastName ?? string.Empty;

            return $"{firstName} {lastName}";
        }
    }

    internal class UserMetadata
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    }
}