using YourCorporation.Shared.Abstractions.Messaging;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesFromNotificationHandlerAsync(IMessage sourceNotification, CancellationToken cancellationToken = default);
    }
}
