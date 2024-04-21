using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal record SessionName
    {
        public string Value { get; } = default!;

        private SessionName() { }

        private SessionName(string value)
        {          
            Value = value;
        }

        public static Result<SessionName> Create(string value) 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return CommonErrors.Empty(ErrorCodes.Sessions.SessionNameErrorCode, "Session name");
            }

            return new SessionName(value);
        }
    }
}
