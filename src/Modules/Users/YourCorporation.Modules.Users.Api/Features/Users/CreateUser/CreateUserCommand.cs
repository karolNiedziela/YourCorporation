using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Users.CreateUser
{
    internal record CreateUserCommand(
        string Email, 
        string Password,
        string FirstName,
        string LastName) : ICommand<Result<Guid>>;
}
