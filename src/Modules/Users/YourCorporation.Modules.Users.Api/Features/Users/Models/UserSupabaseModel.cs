using System.Text.Json.Serialization;

namespace YourCorporation.Modules.Users.Api.Features.Users.Models
{
    internal class UserSupabaseModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
