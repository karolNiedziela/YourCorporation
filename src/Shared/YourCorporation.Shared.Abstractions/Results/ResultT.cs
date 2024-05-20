namespace YourCorporation.Shared.Abstractions.Results
{
    public sealed class Result<TValue> : Result, IResult<TValue>
    {
        private readonly TValue _value = default;

        public TValue Value => _value;

        private Result(Error error) : base(error)
        {
        }

        private Result(List<Error> errors) : base(errors)
        {
        }

        private Result(TValue value) : base()
        {
            _value = value;
        }

        public static implicit operator Result<TValue>(TValue value) => new(value);

        public static implicit operator Result<TValue>(Error error) => new(error);

        public static implicit operator Result<TValue>(List<Error> errors) => new(errors);

        public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onSuccess, Func<List<Error>, TNextValue> onError)
        {
            if (IsFailure)
            {
                return onError(Errors);
            }

            return onSuccess(Value);
        }

        public TNextValue Match<TNextValue>(
            Func<TValue, TNextValue> onSuccess,
            Func<List<Error>, TNextValue> onError,
            Func<Error, TNextValue> onConflict)
        {
            if (!IsFailure)
            {
                return onSuccess(Value);
            }

            if (Errors.Any(x => x.ErrorType == ErrorType.Conflict))
            {
                return onConflict(Errors.First());
            }

            return onError(Errors);
        }

    }
}
