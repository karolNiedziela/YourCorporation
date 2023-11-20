using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal class EventFormRepository : IEventFormRepository
    {
        private readonly FormsDbContext _context;

        public EventFormRepository(FormsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventForm form)
        {
            await _context.EventForms.AddAsync(form);

            await _context.SaveChangesAsync();
        }
    }
}
