namespace shop_backend.Validation
{
    public class Result<T>
    {
        public T? Value { get; }
        public Error? Error { get; }
        public bool IsSuccess => Error == null;

        public Result(T value)
        {
            Value = value;
            Error = null;
        }

        public Result(Error error)
        {
            Error = error;
            Value = default;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(Error error) => new Result<T>(error);
    }
}
