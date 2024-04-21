namespace YourCorporation.Shared.Abstractions.Results
{
    public class Result : IResult
    {
        protected readonly List<Error> _errors = null;

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public List<Error> Errors => IsFailure ? _errors : [Error.None];

        protected Result()
        {
            IsSuccess = true;
        }

        protected Result(Error error)
        {
            _errors = [error];
            IsSuccess = false;
        }

        protected Result(List<Error> errors)
        {
            IsSuccess = false;
            _errors = errors;
        }

        public static Result Success() => new();

        public static implicit operator Result(Error error) => new(error);

        public static implicit operator Result(List<Error> errors) => new(errors);

        public TNextValue Match<TNextValue>(Func<TNextValue> onSuccess, Func<List<Error>, TNextValue> onError)
        {
            if (IsFailure)
            {
                return onError(Errors);
            }

            return onSuccess();
        }
    }
}
