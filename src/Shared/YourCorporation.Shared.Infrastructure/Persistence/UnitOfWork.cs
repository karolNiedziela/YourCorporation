using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly UnitOfWorkTypeRegistry _typeRegistry;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(UnitOfWorkTypeRegistry typeRegistry, IServiceProvider serviceProvider)
        {
            _typeRegistry = typeRegistry;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> SaveChangesAsync(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            var unitOfWorkTypeRegistry = _typeRegistry.Resolve(aggregateRoot);

            var unitOfWorkModuleContext = (IUnitOfWorkModuleContext)_serviceProvider.GetRequiredService(unitOfWorkTypeRegistry);
            return await unitOfWorkModuleContext.SaveChangesAndPublishAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(IAggregateRoot aggregateRoot, IMessage sourceNotification, CancellationToken cancellationToken = default)
        {
            var unitOfWorkTypeRegistry = _typeRegistry.Resolve(aggregateRoot);

            var unitOfWorkModuleContext = (IUnitOfWorkModuleContext)_serviceProvider.GetRequiredService(unitOfWorkTypeRegistry);
            return await unitOfWorkModuleContext.SaveChangesAndPublishAsync(sourceNotification, cancellationToken);
        }
    }
}
