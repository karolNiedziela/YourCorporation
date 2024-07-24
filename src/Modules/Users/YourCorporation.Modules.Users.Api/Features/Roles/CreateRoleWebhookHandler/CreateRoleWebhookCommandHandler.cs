using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YourCorporation.Modules.Users.Api.Database;
using YourCorporation.Modules.Users.Api.Entities;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Roles.CreateRoleWebhookHandler
{
    internal class CreateRoleWebhookCommandHandler : IRequestHandler<CreateRoleWebhookCommand, Result<Guid>>
    {
        private readonly UsersDbContext _context;
        private readonly ILogger<CreateRoleWebhookCommandHandler> _logger;

        public CreateRoleWebhookCommandHandler(UsersDbContext context, ILogger<CreateRoleWebhookCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(CreateRoleWebhookCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.InsertRole.Record.Id, cancellationToken);
            if (role is not null)
            {
                return new Error("Role.AlreadyExists", $"Role with id '{request.InsertRole.Record.Id}' already exists.");
            }

            role = new Role(request.InsertRole.Record.Name, request.InsertRole.Record.Id);

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("New role with '{RoleId}' and name '{RoleName}'.", role.Id, role.Name);

            return role.Id;
        }
    }
}
