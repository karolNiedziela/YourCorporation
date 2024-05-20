﻿using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.Candidates.ValueObjects
{
    internal record BirthDate
    {
        public const int MinimalAge = 16;

        public DateTime Value { get; }

        private BirthDate() { }

        private BirthDate(DateTime value)
        {
            Value = value;
        }

        public static Result<BirthDate> Create(DateTime value, TimeProvider timeProvider)
        {
            var today = timeProvider.GetUtcNow();
            var age = today.Year - value.Year;
            if (value > today.AddYears(-age))
            {
                return ErrorCodes.Candidates.TooYoungError;
            }

            return new BirthDate(value);
        }
    }
}
