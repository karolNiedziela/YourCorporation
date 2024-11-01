using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.Repositories
{
    internal interface IContactRepository
    {
        Task<Contact> GetAsync(PrivateEmail privateEmail);

        Task<Contact> GetAsync(ContactId id);

        void Add(Contact contact);

        void Update(Contact contact);
    }
}
