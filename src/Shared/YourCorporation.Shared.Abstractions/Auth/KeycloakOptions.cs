namespace YourCorporation.Shared.Abstractions.Auth
{
    public class KeycloakOptions
    {
        public const string SectionName = "Keycloak";

        public string Authority { get; set; }

        public string MetadataAddress { get; set; }

        public string Audience { get; set; }

        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }
    }
}
