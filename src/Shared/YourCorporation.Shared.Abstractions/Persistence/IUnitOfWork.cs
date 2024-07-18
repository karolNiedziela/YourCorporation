using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
    }
}
