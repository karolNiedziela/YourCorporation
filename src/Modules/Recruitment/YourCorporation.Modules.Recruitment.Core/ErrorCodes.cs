using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core
{
    internal static class ErrorCodes
    {
        public static class JobApplications
        {
            public static Error EmailError = new("JobApplication.Email", "Invalid email format.");
            public static Error NotFoundError(Guid jobApplicationId) => Error.NotFound("JobApplication.NotFound", $"Job Application with id '{jobApplicationId}' was not found.");
        }

        public static class Contacts
        {
            public static Error InvalidPrivatePhoneFormatError = new("Candidate.PrivatePhone", "Invalid phone number format.");
            public static Error InvalidPrivateEmailFormatError = new("Candidate.InvalidPrivateEmailFormat", "Invalid email format.");
            public static Error InvalidPrivateEmailDomainError = new("Candidate.InvalidPrivateEmailDomain", "Email contains invalid domain.");
            public static Error TooYoungError = new("Candidate.ContactTooYoungError", $"Must be at least {BirthDate.MinimalAge} years old.");
            public static Error InvalidLinkedinUrlError = new("Candidate.ContactInvalidLinkedinUrl", $"Invalid linkedin url, should contain {LinkedinUrl.LinkedingPattern}.");
        }

        public static class RecruitmentQueues
        {
            public static Error AlreadyExistsError(string name) => Error.Conflict("RecruitmentQueue.AlreadyExists", $"Recrutiment Queue with name '{name}' already exists.");
            public static Error NotExistingWorkLocationError => new("RecruitmentQueue.NotExistingWorkLocationError", "One or more work locations were not found.");
            public static Error EmptyName = new("RecruitmentQueue.EmptyName", "Recruitment name cannot be empty.");
        }
    }
}
