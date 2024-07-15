using MediatR;
using YourCorporation.Modules.Events.Core.Events.Repositories;
using YourCorporation.Modules.Events.Core.Speakers.Repositories;
using YourCorporation.Modules.Events.Core.Speakers.ValueObjects;
using YourCorporation.Shared.Abstractions.Persistence;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Application.Commands.Events.AddSpeaker
{
    internal class AddSpeakerCommandHandler : IRequestHandler<AddSpeakerCommand, Result>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpeakerRepository _speakerRepository;

        public AddSpeakerCommandHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork, ISpeakerRepository speakerRepository)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _speakerRepository = speakerRepository;
        }

        public async Task<Result> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            var speaker = await _speakerRepository.GetAsync(request.SpeakerId);
            if (speaker is null)
            {
                return Error.NotFound("Speaker.NotFoundError", $"Speaker with id '{request.SpeakerId}' was not found.");
            }            

            var @event = await _eventRepository.GetAsync(request.EventId);
            if (@event is null)
            {
                return Error.NotFound("Events.NotFoundEventError", $"Event with id '{request.EventId}' was not found.");
            }

            var result = @event.AddSpeaker(new SpeakerId(request.SpeakerId));
            if (result.IsFailure)
            {
                return result.Errors;
            }

            _eventRepository.Update(@event);

            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
