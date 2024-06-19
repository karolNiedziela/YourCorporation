namespace YourCorporation.Shared.Infrastructure.Auth
{
    internal class SupabaseAuthenticationOptions
    {
        public const string SectionName = "SupabaseAuthentication";

        public string JwtSecret { get; set; }

        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }
    }
}
