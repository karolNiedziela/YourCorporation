using Microsoft.EntityFrameworkCore;
using YourCorporation.Modules.Recruitment.Core.Contacts;
using YourCorporation.Modules.Recruitment.Core.Contacts.Repositories;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Infrastructure.EF.Repositories
{
    internal class ContactRepository : IContactRepository
    {
        private readonly DbSet<Contact> _contacts;

        public ContactRepository(RecruitmentDbContext context)
        {
            _contacts = context.Contacts;
        }

        public async Task<Contact?> GetAsync(PrivateEmail privateEmail)
            => await _contacts.FirstOrDefaultAsync(x => x.PrivateEmail == privateEmail);

        public void Add(Contact contact)
            => _contacts.Add(contact);

        public void Update(Contact contact)
            => _contacts.Update(contact);
    }
}
