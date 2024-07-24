using System.Text.Json.Serialization;

namespace YourCorporation.Modules.Users.Api.Features.Users.Models
{
    internal class UserRoleSupabaseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("role_id")]
        public Guid RoleId { get; set; }

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }
    }
}
