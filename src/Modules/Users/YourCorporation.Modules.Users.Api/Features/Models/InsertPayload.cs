using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace YourCorporation.Modules.Users.Api.Features.Models
{
    internal class InsertPayload<T>
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("table")]
        public string Table { get; set; }

        [JsonPropertyName("schema")]
        public string Schema { get; set; }

        [JsonPropertyName("record")]
        public T Record { get; set; }

        [JsonProperty("old_record")]
        public object OldRecord { get; set; }
    }
}
