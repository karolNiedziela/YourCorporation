namespace YourCorporation.Shared.Abstractions.Persistence
{
    public interface IUnitOfWorkProcessor
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
