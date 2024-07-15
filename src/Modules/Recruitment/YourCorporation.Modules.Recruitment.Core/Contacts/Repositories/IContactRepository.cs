using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;

namespace YourCorporation.Modules.Recruitment.Core.Contacts.Repositories
{
    internal interface IContactRepository
    {
        Task<Contact> GetAsync(PrivateEmail privateEmail);

        void Add(Contact candidate);
    }
}
