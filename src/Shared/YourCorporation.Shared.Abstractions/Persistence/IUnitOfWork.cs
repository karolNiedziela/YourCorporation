using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(IAggregateRoot aggregateRoot, IMessage sourceNotification, CancellationToken cancellationToken = default);
    }
}
