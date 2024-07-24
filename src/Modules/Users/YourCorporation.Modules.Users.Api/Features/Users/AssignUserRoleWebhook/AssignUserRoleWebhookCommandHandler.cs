using MediatR;
using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Users.Api.Database;
using YourCorporation.Modules.Users.Api.Entities;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Users.AssignUserRoleWebhook
{
    internal class AssignUserRoleWebhookCommandHandler : IRequestHandler<AssignUserRoleWebhookCommand, Result>
    {
        private readonly UsersDbContext _context;

        public AssignUserRoleWebhookCommandHandler(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AssignUserRoleWebhookCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.SystemUsers.FirstOrDefaultAsync(x => x.Id == request.InsertUserRole.Record.UserId, cancellationToken);
            if (user is null)
            {
                return new Error("SystemUsers.NotFound", $"User with id '{request.InsertUserRole.Record.UserId}' was not found.");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.InsertUserRole.Record.RoleId, cancellationToken);
            if (role is null)
            {
                return new Error("Roles.NotFound", $"Role with id '{request.InsertUserRole.Record.RoleId}' was not found.");
            }

            user.Roles.Add(role);

            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
