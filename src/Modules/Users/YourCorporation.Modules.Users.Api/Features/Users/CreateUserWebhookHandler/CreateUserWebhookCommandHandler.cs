using MediatR;
using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Users.Api.Database;
using YourCorporation.Modules.Users.Api.Entities;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Users.CreateUserWebhookHandler
{
    internal class CreateUserWebhookCommandHandler : IRequestHandler<CreateUserWebhookComand, Result>
    {
        private readonly UsersDbContext _context;

        public CreateUserWebhookCommandHandler(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateUserWebhookComand request, CancellationToken cancellationToken)
        {
            var user = await _context.SystemUsers.FirstOrDefaultAsync(x => x.Id == request.InsertUserSupabase.Record.Id, cancellationToken);
            if (user is not null)
            {
                return new Error("SystemUsers.AlreadyExists", $"User with id '{request.InsertUserSupabase.Record.Id}' already exists.");
            }

            user = new SystemUser(
                request.InsertUserSupabase.Record.FirstName,
                request.InsertUserSupabase.Record.LastName,
                request.InsertUserSupabase.Record.Email,
                request.InsertUserSupabase.Record.Id
                );

            await _context.SystemUsers.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
