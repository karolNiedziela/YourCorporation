using YourCorporation.Shared.Abstractions.Exceptions.Common;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record EventName
    {
        public string Value { get; } = default!;

        public EventName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyValueException(ErrorCodes.Events.EmptyEventNameError);
            }

            Value = value;
        }
    }
}
