using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;

namespace YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions
{
    internal class EventSubmission : FormSubmissionBase
    {
        public EventForm EventForm { get; private set; }

        public Guid EventFormId { get; private set; }

        public EventSubmission(Guid id, Guid eventFormId, string firstName, string lastName, string email) 
            : base(id, firstName, lastName, email)
        {
            EventFormId = eventFormId;
        }
    }
}
