using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Users.Api.Entities
{
    internal class Permission : Entity<Guid>
    {
        public string Name { get; set; }

        public List<Role> Roles { get; set; } = [];

        private Permission() { }

        public Permission(string name, Guid id) : base(id)
        {
            Name = name;
        }
    }
}
