namespace YourCorporation.Shared.Abstractions.Persistence
{
    public interface IUnitOfWorkModuleContext
    {
        Task<int> SaveChangesAndPublishAsync(CancellationToken cancellationToken = default);
    }
}