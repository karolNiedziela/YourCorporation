using YourCorporation.Modules.Users.Api.Features.Models;
using YourCorporation.Modules.Users.Api.Features.Users.Models;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Users.CreateUserWebhookHandler
{
    internal record CreateUserWebhookComand(InsertPayload<UserSupabaseModel> InsertUserSupabase) : ICommand<Result>;
}
