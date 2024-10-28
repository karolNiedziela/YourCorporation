namespace YourCorporation.Shared.Abstractions.Auth
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionAsync(Guid userId);
    }
}
