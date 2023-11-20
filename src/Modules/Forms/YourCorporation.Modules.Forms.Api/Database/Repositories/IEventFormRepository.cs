using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;

namespace YourCorporation.Modules.Forms.Api.Database.Repositories
{
    internal interface IEventFormRepository
    {
        Task AddAsync(EventForm form);
    }
}
