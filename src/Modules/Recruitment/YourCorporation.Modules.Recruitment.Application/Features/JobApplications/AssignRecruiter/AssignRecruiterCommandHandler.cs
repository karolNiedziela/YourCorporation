using MediatR;
using YourCorporation.Modules.Recruitment.Core;
using YourCorporation.Modules.Recruitment.Core.JobApplications.Repositories;
using YourCorporation.Modules.Recruitment.Core.JobApplications.ValueObjects;
using YourCorporation.Shared.Abstractions.Contexts;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Modules.Recruitment.Application.Features.JobApplications.AssignRecruiter
{
    internal class AssignRecruiterCommandHandler : IRequestHandler<AssignRecruiterCommand, Result<AssignRecruiterResponse>>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IContext _context;

        public AssignRecruiterCommandHandler(IJobApplicationRepository jobApplicationRepository, IContext context)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _context = context;
        }

        public async Task<Result<AssignRecruiterResponse>> Handle(AssignRecruiterCommand request, CancellationToken cancellationToken)
        {
            var jobApplication = await _jobApplicationRepository.GetAsync(request.JobApplicationId);
            if (jobApplication is null)
            {
                return ErrorCodes.JobApplications.NotFoundError(request.JobApplicationId);
            }

            if (jobApplication.AssignedRecruiter is not null)
            {
                return ErrorCodes.JobApplications.RecruiterAlreadyAssigned;
            }

            var assignedRecruiter = AssignedRecruiter.Create(_context.Identity.Id, _context.Identity.FullName);
            jobApplication.AssignRecruiter(assignedRecruiter);

            _jobApplicationRepository.Update(jobApplication);

            return new AssignRecruiterResponse(assignedRecruiter.Id!.Value, assignedRecruiter.FullName);
        }
    }
}
