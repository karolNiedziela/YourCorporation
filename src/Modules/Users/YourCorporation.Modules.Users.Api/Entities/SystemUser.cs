using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Users.Api.Entities
{
    internal class SystemUser : Entity<Guid>
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public List<Role> Roles { get; private set; } = [];

        private SystemUser() { }

        public SystemUser(string firstName, string lastName, string email, Guid id) : base(id) 
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
