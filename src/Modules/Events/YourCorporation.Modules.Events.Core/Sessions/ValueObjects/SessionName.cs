using YourCorporation.Shared.Abstractions.Exceptions.Common;

namespace YourCorporation.Modules.Events.Core.Sessions.ValueObjects
{
    internal record SessionName
    {
        public string Value { get; } = default!;

        private SessionName() { }

        public SessionName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyValueException(ErrorCodes.Sessions.SessionNameError);
            }

            Value = value;
        }
    }
}
