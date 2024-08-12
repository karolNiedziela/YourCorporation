namespace YourCorporation.Shared.Abstractions.Auth
{
    public class SupabaseAuthenticationOptions
    {
        public const string SectionName = "SupabaseAuthentication";

        public string JwtSecret { get; set; }

        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }

        public string Signature { get; set; }
    }
}
