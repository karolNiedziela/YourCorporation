using MediatR;
using Microsoft.Extensions.Logging;
using Supabase.Gotrue;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Users.Api.Features.Users.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly TimeProvider _timeProvider;

        public CreateUserCommandHandler(Supabase.Client supabaseClient, ILogger<CreateUserCommandHandler> logger, TimeProvider timeProvider)
        {
            _supabaseClient = supabaseClient;
            _logger = logger;
            _timeProvider = timeProvider;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _supabaseClient.Auth.SignUp(
                request.Email, 
                request.Password,                
                new SignUpOptions
                {
                    Data = new Dictionary<string, object>
                    {
                        { "first_name", request.FirstName },
                        { "last_name", request.LastName },
                    }
                });

                // Confirm email is turn off, so session is not null
                // When confirm is enabled, session would be null
                // More information here: https://supabase.com/docs/reference/csharp/auth-signup
                return Guid.Parse(session.User.Id)!;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when creating supabase user: {Message}", ex.Message);
                return new Error("Users.CreateUser", ex.Message, ErrorType.ExternalApi);
            }          
        }
    }
}
