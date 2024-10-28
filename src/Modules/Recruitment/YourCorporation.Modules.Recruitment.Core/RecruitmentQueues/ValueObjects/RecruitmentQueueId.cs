namespace YourCorporation.Modules.Recruitment.Core.RecruitmentQueues.ValueObjects
{
    internal record RecruitmentQueueId(Guid Value)
    {
        public static RecruitmentQueueId New() => new(Guid.NewGuid());

        public static implicit operator Guid(RecruitmentQueueId recruitmentQueueId) => recruitmentQueueId.Value;
    }
}
