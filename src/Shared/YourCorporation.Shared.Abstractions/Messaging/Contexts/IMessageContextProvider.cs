namespace YourCorporation.Shared.Abstractions.Messaging.Contexts
{
    public interface IMessageContextProvider
    {
        IMessageContext Get(IMessage message);
    }
}
