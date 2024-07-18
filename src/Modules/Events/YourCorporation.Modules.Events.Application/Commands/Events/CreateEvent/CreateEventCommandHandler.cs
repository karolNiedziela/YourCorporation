using MediatR;
using YourCorporation.Modules.Events.Core.Events;
using YourCorporation.Modules.Events.Core.Events.Repositories;
using YourCorporation.Modules.Events.Core.Events.ValueObjects;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Application.Commands.Events.CreateEvent
{
    internal class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<Guid>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventName = EventName.Create(request.Name);
            var eventDescripton = EventDescription.Create(request.Description);
            var begginingAndEndOfEvent = BegginingAndEndOfEvent.Create(request.StartTime, request.EndTime);
            var eventLimits = EventLimits.Create(request.AttendeesLimit);

            var result = ResultHelper.AggregateErrors(eventName, eventDescripton, begginingAndEndOfEvent, eventLimits);
            if (result.IsFailure)
            {
                return result.Errors;
            }

            var @event = new Event(
               eventName.Value,
               eventDescripton.Value,
               request.Category,
               request.Mode,
               begginingAndEndOfEvent.Value,
               eventLimits.Value);

            _eventRepository.Add(@event);

            await _unitOfWork.SaveChangesAsync(@event, cancellationToken);

            return @event.Id.Value;
        }
    }
}
