namespace shop_backend.Validation
{
    public class Result<T>
    {
        public T? Value { get; }
        public Error? Error { get; }
        public string? Location { get; }
        public bool IsSuccess => Error == null;

        public Result(T value)
        {
            Value = value;
            Error = null;
            Location = String.Empty;
        }

        public Result(T value, string location)
        {
            Value = value;
            Error = null;
            Location = location;
        }

        public Result(Error error)
        {
            Error = error;
            Value = default;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Success(T value, string location) => new Result<T>(value, location);
        public static Result<T> Failure(Error error) => new Result<T>(error);
    }
}
