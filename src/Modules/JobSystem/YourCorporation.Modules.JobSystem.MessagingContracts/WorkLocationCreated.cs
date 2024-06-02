using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.JobSystem.MessagingContracts
{
    public record WorkLocationCreated(Guid Id, string Name, string Code) : IMessage;
}
