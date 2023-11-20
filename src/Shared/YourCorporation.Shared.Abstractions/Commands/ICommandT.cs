using MediatR;

namespace YourCorporation.Shared.Abstractions.Commands
{
    public interface ICommand<TResponse> : IRequest<TResponse>, ICommandBase
    {
    }
}
