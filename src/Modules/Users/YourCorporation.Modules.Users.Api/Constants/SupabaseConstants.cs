namespace YourCorporation.Modules.Users.Api.Constants
{
    internal static class SupabaseConstants
    {
        public static class Tables
        {
            public static class Permissions
            {
                public const string TableName = "permissions";
                public const string IdColumnName = "id";
                public const string NameColumnName = "name";
            }

            public static class Roles
            {
                public const string TableName = "roles";
                public const string IdColumnName = "id";
                public const string NameColumnName = "name";
            }
        }
    }
}
