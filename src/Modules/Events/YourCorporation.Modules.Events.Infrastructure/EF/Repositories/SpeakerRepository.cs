using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Events.Core.Speakers;
using YourCorporation.Modules.Events.Core.Speakers.Repositories;

namespace YourCorporation.Modules.Events.Infrastructure.EF.Repositories
{
    internal class SpeakerRepository : ISpeakerRepository
    {
        private readonly DbSet<Speaker> _speakers;

        public SpeakerRepository(EventsDbContext context)
        {
            _speakers = context.Speakers;
        }

        public Task<Speaker> GetAsync(Guid speakerId)
             => _speakers.FirstOrDefaultAsync(x => x.Id == speakerId);
    }
}
