namespace YourCorporation.Shared.Abstractions.Extensions
{
    public static class ModuleExtensions
    {
        public static string GetModuleName(this object value)
            => value?.GetType().GetModuleName() ?? string.Empty;

        public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.Contains(namespacePart)
                ? type.Namespace.Split(".")[splitIndex]
                : string.Empty;
        }
    }
}
