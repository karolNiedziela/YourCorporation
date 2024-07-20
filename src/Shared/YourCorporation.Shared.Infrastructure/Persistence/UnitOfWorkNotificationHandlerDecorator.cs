using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Messaging.Contexts;
using YourCorporation.Shared.Abstractions.Persistence;

namespace YourCorporation.Shared.Infrastructure.Persistence
{
    public class UnitOfWorkNotificationHandlerDecorator<TMessage> : INotificationHandler<TMessage>
        where TMessage : IMessage
    {
        private readonly INotificationHandler<TMessage> _innerHandler;
        private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkNotificationHandlerDecorator(INotificationHandler<TMessage> innerHandler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
        {
            _innerHandler = innerHandler;
            _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(TMessage notification, CancellationToken cancellationToken)
        {
            // Pre-processing logic

            // Call the original handler
            await _innerHandler.Handle(notification, cancellationToken);

            // Post-processing logic
            var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve(_innerHandler.GetType());
            if (unitOfWorkType is null)
            {
                return;
            }

            var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);

            await unitOfWork.SaveChangesFromNotificationHandlerAsync(notification, cancellationToken);
        }
    }
}
