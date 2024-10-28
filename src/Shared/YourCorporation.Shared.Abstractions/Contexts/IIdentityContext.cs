namespace YourCorporation.Shared.Abstractions.Contexts
{
    public interface IIdentityContext
    {
        bool IsAuthenticated { get; }

        Guid Id { get; }

        string[] Roles { get; }

        Dictionary<string, IEnumerable<string>> Claims { get; }

        bool IsSystemAdministrator();
    }
}
