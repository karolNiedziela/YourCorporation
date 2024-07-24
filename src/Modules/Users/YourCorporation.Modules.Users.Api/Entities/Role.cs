using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Users.Api.Entities
{
    internal class Role : Entity<Guid>
    {
        public string Name { get; private set; }

        public List<SystemUser> SystemUsers { get; private set; } = [];

        public List<Permission> Permissions { get; private set; } = [];

        private Role() { }

        public Role(string name, Guid id): base(id)
        {
            Name = name;
        }
    }
}
