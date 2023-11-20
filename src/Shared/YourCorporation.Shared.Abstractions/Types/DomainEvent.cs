namespace YourCorporation.Shared.Abstractions.Types
{
    public record DomainEvent(Guid Id) : IDomainEvent;
}
