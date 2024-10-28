namespace YourCorporation.Shared.Abstractions.Contexts
{
    public interface IContext
    {
        Guid RequestId { get; }

        Guid CorrelationId { get; }

        string TraceId { get; }

        IIdentityContext Identity { get; }
    }
}
