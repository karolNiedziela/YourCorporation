using YourCorporation.Shared.Abstractions.Exceptions.Common;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record EventDescription
    {
        public const int MaximimumLength = 2000;

        public string Value { get; }

        public EventDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyValueException(ErrorCodes.Events.EmptyEventDescriptionError);
            }

            if (value.Length > 2000)
            {
                throw new MaxLengthException(ErrorCodes.Events.MaxLengthEventDescription, MaximimumLength);
            }

            Value = value;
        }
    }
}
