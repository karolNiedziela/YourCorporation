namespace YourCorporation.Modules.Recruitment.Core.WorkLocations
{
    internal readonly record struct WorkLocationId(Guid Value)
    {
        public static WorkLocationId New() => new(Guid.NewGuid());

        public static implicit operator Guid(WorkLocationId workLocationId) => workLocationId.Value;
    }
}
