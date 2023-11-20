using FluentValidation;
using MediatR;
using YourCorporation.Shared.Abstractions.Commands;

namespace YourCorporation.Shared.Abstractions.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommandBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new ValidationError
                {
                    PropertyName = validationFailure.PropertyName,
                    Message = validationFailure.ErrorMessage
                })
                .ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            var response = await next();

            return response;
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(IReadOnlyCollection<ValidationError> errors)
            : base("Validation failed")
        {
            Errors = errors;
        }

        public IReadOnlyCollection<ValidationError> Errors { get; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }

        public required string Message { get; set; }
    }
}
