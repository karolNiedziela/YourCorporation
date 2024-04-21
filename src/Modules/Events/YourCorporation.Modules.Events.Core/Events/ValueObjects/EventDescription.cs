using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal record EventDescription
    {
        public const int MaximimumLength = 2000;

        public string Value { get; }

        private EventDescription(string value)
        {           
            Value = value;
        }

        public static Result<EventDescription> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return CommonErrors.Empty(ErrorCodes.Events.EmptyEventDescriptionErrorCode, "Event description");
            }

            if (value.Length > MaximimumLength)
            {
                return CommonErrors.MaxLength(ErrorCodes.Events.MaxLengthEventDescriptionErrorCode, MaximimumLength, "Event description");
            }

            return new EventDescription(value);
        }
    }
}
