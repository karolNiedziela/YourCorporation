using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Forms.Api.Entities.Forms
{
    internal abstract class FormBase : Entity<Guid>
    {
        public string Name { get; protected set; }

        public bool IsUniqueSubmission { get; protected set; }

        protected FormBase(
            Guid id,
            string name,
            bool isUniqueSubmission = false) : base(id)
        {
            Name = name;
            IsUniqueSubmission = isUniqueSubmission;
        }
    }
}
