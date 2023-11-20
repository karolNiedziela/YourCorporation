using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions
{
    internal abstract class FormSubmissionBase : Entity<Guid>
    {
        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        public string Email { get; protected set; }

        protected FormSubmissionBase(Guid id, string firstName, string lastName, string email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
