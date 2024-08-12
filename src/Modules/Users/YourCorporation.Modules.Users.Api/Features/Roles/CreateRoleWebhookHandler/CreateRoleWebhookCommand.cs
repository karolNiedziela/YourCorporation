using YourCorporation.Modules.Users.Api.Features.Models;
using YourCorporation.Modules.Users.Api.Features.Users.Models;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Roles.CreateRoleWebhookHandler
{
    internal record CreateRoleWebhookCommand(InsertPayload<RoleSupabaseModel> InsertRole) : ICommand<Result<Guid>>;
}
