namespace YourCorporation.Modules.Events.Core.Speakers.Repositories
{
    internal interface ISpeakerRepository
    {
        Task<Speaker> GetAsync(Guid speakerId);
    }
}
