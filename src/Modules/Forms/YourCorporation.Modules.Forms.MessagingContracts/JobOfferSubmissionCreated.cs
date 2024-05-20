namespace YourCorporation.Modules.Forms.MessagingContracts
{
    public record JobOfferSubmissionCreated(Guid JobOfferSubmissionId, string FirstName, string LastName, string Email, IEnumerable<Guid> ChosenWorkLocationIds);
}
