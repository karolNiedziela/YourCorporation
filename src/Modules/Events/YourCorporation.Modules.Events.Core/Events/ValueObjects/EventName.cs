using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record EventName
    {
        public string Value { get; } = default!;

        private EventName(string value)
        {           
            Value = value;
        }

        public static Result<EventName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return ErrorCodes.Events.EmptyEventNameError;
            }

            return new EventName(value);
        }
    }
}
