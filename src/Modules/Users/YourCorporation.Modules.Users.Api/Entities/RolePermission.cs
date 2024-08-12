namespace YourCorporation.Modules.Users.Api.Entities
{
    internal class RolePermission
    {
        public Guid RoleId { get; private set; }

        public Guid PermissionId { get; private set; }
    }
}
