using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Core
{
    internal static class ErrorCodes
    {
        public static class JobApplications
        {
            public static Error EmailError = new("JobApplication.Email", "Invalid email format.");
        }

        public static class Candidates
        {
            public static Error InvalidPrivatePhoneFormatError = new("Candidate.PrivatePhone", "Invalid phone number format.");
            public static Error InvalidPrivateEmailFormatError = new("Candidate.InvalidPrivateEmailFormat", "Invalid email format.");
            public static Error InvalidPrivateEmailDomainError = new("Candidate.InvalidPrivateEmailDomain", "Email contains invalid domain.");
            public static Error TooYoungError = new("Candidate.ContactTooYoungError", $"Must be at least {BirthDate.MinimalAge} years old.");
            public static Error InvalidLinkedinUrlError = new("Candidate.ContactInvalidLinkedinUrl", $"Invalid linkedin url, should contain {LinkedinUrl.LinkedingPattern}.");
        }
    }
}
