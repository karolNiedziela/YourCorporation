using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    public class TransactionalPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : ICommandBase
        where TResponse : IResult
    {
        private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
        private readonly IServiceProvider _serviceProvider;

        public TransactionalPostProcessor(UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
        {
            _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
            _serviceProvider = serviceProvider;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve<TRequest>();
            if (unitOfWorkType is null)
            {               
                return;
            }

            var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
