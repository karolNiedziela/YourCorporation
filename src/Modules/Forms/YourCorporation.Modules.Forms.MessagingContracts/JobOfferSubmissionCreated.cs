using YourCorporation.Shared.Abstractions.Messaging;

namespace YourCorporation.Modules.Forms.MessagingContracts
{
    public record JobOfferSubmissionCreated(
        Guid JobOfferSubmissionId, 
        string FirstName, 
        string LastName, 
        string CVUrl, 
        string Email, 
        Guid JobOfferId,
        IEnumerable<Guid> ChosenWorkLocationIds) : IIntegrationEvent;
}
