using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace YourCorporation.Shared.Abstractions.Validators.FluentValidators
{
    public class PDFValidator : AbstractValidator<IFormFile>
    {
        public PDFValidator()
        {
            RuleFor(x => x.ContentType)
                .NotNull()
                .Must(x => x.Equals(MediaTypeNames.Application.Pdf))
                .WithMessage("CV must be in PDF format.");
        }
    }
}
