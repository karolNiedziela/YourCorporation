﻿using FluentValidation;
using MediatR;
using YourCorporation.Shared.Abstractions.Commands;
using YourCorporation.Shared.Abstractions.Results;

namespace YourCorporation.Shared.Abstractions.MediatR.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommandBase
        where TResponse : IResult
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
             .SelectMany(x => x.Errors)
             .ToList()
             .ConvertAll(error => Error.Validation
             (
                 errorCode: error.PropertyName,
                 message: error.ErrorMessage
             ));

            if (errors.Any())
            {
                return (dynamic)errors;
            }

            return await next();
        }
    }   
}
