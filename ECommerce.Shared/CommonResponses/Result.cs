namespace ECommerce.Shared.CommonResponses
{
    public class Result
    {
        private readonly List<Error> errors = new();
        public bool IsSuccess => errors.Count == 0;
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors => errors;

        // Success
        protected Result()
        {

        }

        // Failure with single error
        protected Result(Error error)
        {
            errors.Add(error);
        }

        // Failure with multiple errors
        protected Result(IEnumerable<Error> errors)
        {
            this.errors.AddRange(errors);
        }
        public static Result Ok() => new();
        public static Result Fail(Error error) => new(error);
        public static Result Fail(IEnumerable<Error> errors) => new(errors);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value
            => IsSuccess ? _value
            : throw new InvalidOperationException(
                "Cannot access the value of a failed result."
                );

        private Result(TValue value)
        {
            _value = value;
        }

        private Result(Error error)
            : base(error)
        {
            _value = default!;
        }

        private Result(IEnumerable<Error> errors)
            : base(errors)
        {
            _value = default!;
        }


        public static Result<TValue> Ok(TValue value) => new(value);
        public new static Result<TValue> Fail(Error error) => new(error);
        public new static Result<TValue> Fail(IEnumerable<Error> errors) => new(errors);


        public static implicit operator Result<TValue>(TValue value) => Ok(value);

        public static implicit operator Result<TValue>(Error error) => Fail(error);

        public static implicit operator Result<TValue>(List<Error> errors) => Fail(errors);
    }
}
