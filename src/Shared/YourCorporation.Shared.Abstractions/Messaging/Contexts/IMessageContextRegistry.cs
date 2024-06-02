namespace YourCorporation.Shared.Abstractions.Messaging.Contexts
{
    public interface IMessageContextRegistry
    {
        void Set(IMessage message, IMessageContext context);
    }
}
