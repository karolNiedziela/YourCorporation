using MediatR;
using YourCorporation.Modules.Events.MessagingContracts;
using YourCorporation.Modules.Forms.Api.Database.Repositories;
using YourCorporation.Modules.Forms.Api.Entities.Forms.EventForms;

namespace YourCorporation.Modules.Forms.Api.IntegrationEventsHandlers.Handlers
{
    internal class EventWentLiveHandler : INotificationHandler<EventWentLive>
    {
        private readonly IEventFormRepository _eventFormRepository;

        public EventWentLiveHandler(IEventFormRepository eventFormRepository)
        {
            _eventFormRepository = eventFormRepository;
        }

        public async Task Handle(EventWentLive notification, CancellationToken cancellationToken)
        {
            var eventForm = new EventForm(Guid.NewGuid(),
               notification.EventId,
               notification.Name,
               "Description",
               notification.StartTime,
               notification.EndTime
               );

            await _eventFormRepository.AddAsync(eventForm);
        }
    }
}
