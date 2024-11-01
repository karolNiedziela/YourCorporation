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
            public static Error RecruiterAlreadyAssigned => new("JobApplication.RecruiterAlreadyAssigned", "Job application has already assigned recruiter.");
        }

        public static class Contacts
        {
            public static Error InvalidPrivatePhoneFormatError = new("Contacts.PrivatePhone", "Invalid phone number format.");
            public static Error InvalidPrivateEmailFormatError = new("Contacts.InvalidPrivateEmailFormat", "Invalid email format.");
            public static Error InvalidPrivateEmailDomainError = new("Contacts.InvalidPrivateEmailDomain", "Email contains invalid domain.");
            public static Error TooYoungError = new("Contacts.ContactTooYoungError", $"Must be at least {BirthDate.MinimalAge} years old.");
            public static Error InvalidLinkedinUrlError = new("Contacts.ContactInvalidLinkedinUrl", $"Invalid linkedin url, should contain {LinkedinUrl.LinkedingPattern}.");

            public static Error NotFoundError(Guid contactId) => Error.NotFound("Contacts.NotFound", $"Contact with id '{contactId}' was not found.");
        }

        public static class RecruitmentQueues
        {
            public static Error AlreadyExistsError(string name) => Error.Conflict("RecruitmentQueue.AlreadyExists", $"Recrutiment Queue with name '{name}' already exists.");
            public static Error NotExistingWorkLocationError = new("RecruitmentQueue.NotExistingWorkLocationError", "One or more work locations were not found.");
            public static Error EmptyName = new("RecruitmentQueue.EmptyName", "Recruitment name cannot be empty.");
        }

        public static class ContactJobApplicationResult
        {
            public static Error AlreadyExistsError(Guid jobApplicationId) => Error.Conflict("ContactJobApplicationResult.AlreadyExists", $"Contact Job Application Result with job application id '{jobApplicationId}' already exists.");

            public static Error GivenRejectedReasonError => new Error("ContactJobApplicationResult.GivenRejectedReason", "Rejected reason cannot be given when Application Decision is different to rejected.");
            public static Error NoRejectedReasonError => new Error("ContactJobApplicationResult.NoRejectedReason", "Rejected reason is required when Application Decision is rejected.");
        }
    }
}
