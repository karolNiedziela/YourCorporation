using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Users.Api.Database;
using YourCorporation.Shared.Abstractions.Auth;

namespace YourCorporation.Modules.Users.Api.Services
{
    internal class PermissionService : IPermissionService
    {
        private readonly UsersDbContext _context;

        public PermissionService(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<HashSet<string>> GetPermissionAsync(Guid userId)
        {
            var roles = await _context.SystemUsers
                .Include(x => x.Roles)
                .ThenInclude(x => x.Permissions)
                .Where(x => x.Id == userId)
                .Select(x => x.Roles)
                .ToArrayAsync();

            return roles.SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Name)
                .ToHashSet();
        }
    }
}
