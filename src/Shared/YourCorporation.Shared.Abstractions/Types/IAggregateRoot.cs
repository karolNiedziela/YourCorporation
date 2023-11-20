namespace YourCorporation.Shared.Abstractions.Types
{
    public interface IAggregateRoot
    {
        IReadOnlyList<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
