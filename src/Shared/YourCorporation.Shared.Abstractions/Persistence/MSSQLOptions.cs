namespace YourCorporation.Shared.Abstractions.Persistence
{
    public class MSSQLOptions
    {
        public const string SectionName = "MSSQL";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
