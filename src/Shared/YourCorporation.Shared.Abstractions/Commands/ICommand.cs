using MediatR;

namespace YourCorporation.Shared.Abstractions.Commands
{
    public interface ICommand : IRequest, ICommandBase
    {
    }
}
