namespace YourCorporation.Shared.Infrastructure.SupabaseFeatures
{
    internal class SupabaseClientOptions
    {
        public const string SectionName = "SupabaseClient";

        public string Url { get; set; }

        public string Key { get; set; }
    }
}
