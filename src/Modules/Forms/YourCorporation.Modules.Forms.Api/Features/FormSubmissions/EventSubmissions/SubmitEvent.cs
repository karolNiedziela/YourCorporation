using MediatR;
using YourCorporation.Shared.Abstractions.Commands;

namespace YourCorporation.Modules.Forms.Api.Features.FormSubmissions.EventSubmissions
{
    internal static class SubmitEvent
    {
        public record Command(Guid EventFormId, string FirstName, string LastName, string Email) : ICommand;


        public class CommandHandler : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {

            }
        }
    }
}
