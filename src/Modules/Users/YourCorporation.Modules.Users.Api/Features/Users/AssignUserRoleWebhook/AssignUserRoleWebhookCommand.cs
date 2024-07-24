using YourCorporation.Modules.Users.Api.Features.Models;
using YourCorporation.Modules.Users.Api.Features.Users.Models;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Users.AssignUserRoleWebhook
{
    internal record AssignUserRoleWebhookCommand(InsertPayload<UserRoleSupabaseModel> InsertUserRole) : ICommand<Result>;    
}
