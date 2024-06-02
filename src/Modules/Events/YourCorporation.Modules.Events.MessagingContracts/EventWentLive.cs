using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Events.MessagingContracts
{
    public record EventWentLive(
        Guid EventId, 
        string Name, 
        DateTimeOffset StartTime, 
        DateTimeOffset EndTime) : IMessage;
}