using MediatR;
using YourCorporation.Modules.Recruitment.Core;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults;
using YourCorporation.Modules.Recruitment.Core.ContactJobApplicationResults.Repositories;
using YourCorporation.Modules.Recruitment.Core.Contacts.Repositories;
using YourCorporation.Modules.Recruitment.Core.Contacts.ValueObjects;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Application.Features.ContactJobApplicationResults.CreateContactJobApplicationResult
{
    internal class CreateContactJobApplicationResultCommandHandler : IRequestHandler<CreateContactJobApplicationResultCommand, Result<Guid>>
    {
        private readonly IContactJobApplicationResultRepository _contactJobApplicationResultRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public CreateContactJobApplicationResultCommandHandler(IContactJobApplicationResultRepository contactJobApplicationResultRepository, IContactRepository contactRepository, IJobApplicationRepository jobApplicationRepository)
        {
            _contactJobApplicationResultRepository = contactJobApplicationResultRepository;
            _contactRepository = contactRepository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<Result<Guid>> Handle(CreateContactJobApplicationResultCommand request, CancellationToken cancellationToken)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(request.JobApplicationId);
            if (jobApplication is null)
            {
                return ErrorCodes.JobApplications.NotFoundError(request.JobApplicationId);
            }

            var contactId = new ContactId(request.ContactId);
            var contact = await _contactRepository.GetAsync(contactId);
            if (contact is null)
            {
                return ErrorCodes.Contacts.NotFoundError(request.ContactId);
            }

            var jobApplicationId = new JobApplicationId(request.JobApplicationId);
            var existingContactJobApplicationResult = await _contactJobApplicationResultRepository.GetAsync(jobApplicationId);
            if (existingContactJobApplicationResult is not null)
            {
                return ErrorCodes.ContactJobApplicationResult.AlreadyExistsError(request.JobApplicationId);
            }

            var resultContactJobApplicationResult = ContactJobApplicationResult.Create(
                jobApplicationId,
                contactId,
                request.ApplicationDecision,
                request.RejectedReason);

            if (resultContactJobApplicationResult.IsFailure)
            {
                return resultContactJobApplicationResult.Errors;
            }

            _contactJobApplicationResultRepository.Add(resultContactJobApplicationResult.Value);

            return resultContactJobApplicationResult.Value.Id.Value;
        }
    }
}
