using System.Text.Json.Serialization;

namespace YourCorporation.Modules.Users.Api.Features.Users.Models
{
    internal class RoleSupabaseModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
