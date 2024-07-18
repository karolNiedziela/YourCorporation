using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Shared.Abstractions.Persistence
{
    public interface IUnitOfWorkModuleContext
    {
        Task<int> SaveChangesAndPublishAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAndPublishAsync(IMessage sourceNotification, CancellationToken cancellationToken = default);
    }
}