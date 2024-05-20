using YourCorporation.Modules.Forms.Api.Entities.FormSubmissions.EventSubmissions;

namespace YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms
{
    internal class EventForm : FormBase
    {
        public Guid EventId { get; private set; }

        public string EventName { get; private set; }

        public string EventDescription { get; private set; }

        public DateTimeOffset StartTime { get; private set; }

        public DateTimeOffset EndTime { get; private set; }

        public IEnumerable<EventSubmission> Submissions { get; private set; } = [];

        private EventForm() : base() { }

        public EventForm(
            Guid id,
            Guid eventId, 
            string eventName, 
            string eventDescription,
            DateTimeOffset startTime, 
            DateTimeOffset endTime) 
            : base(id, eventName, true)
        {
            EventId = eventId;
            EventName = eventName;
            EventName = eventDescription;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
