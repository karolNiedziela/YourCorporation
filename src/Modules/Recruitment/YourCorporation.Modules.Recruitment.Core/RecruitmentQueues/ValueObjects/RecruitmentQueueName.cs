using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects
{
    internal record RecruitmentQueueName
    {
        public const int MaxLength = 100;

        public string Value { get; } = default!;

        private RecruitmentQueueName() { }

        private RecruitmentQueueName(string value)
        {
            Value = value;
        }

        public static Result<RecruitmentQueueName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return ErrorCodes.RecruitmentQueues.EmptyName;
            }

            if (value.Length > MaxLength)
            {
                return CommonErrors.MaxLength("RecruitmentQueue.NameMaxLength", MaxLength, "Name");
            }

            return new RecruitmentQueueName(value);
        }

        public static implicit operator string(RecruitmentQueueName name) => name.Value;

        public static implicit operator RecruitmentQueueName(string name) => new(name);
    }
}
