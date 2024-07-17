using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects
{
    internal class JobApplicationId(Guid value) : StronglyTypedId(value)
    {
    }
}
