namespace YourCorporation.Modules.JobSystem.MessagingContracts
{
    public record JobOfferPublished(Guid JobOfferId, string Name, IEnumerable<Guid> WorkLocationIds);
}
