namespace shop_backend.Validation
{
    public class Error
    {
        public string Message { get; }
        public int StatusCode { get; }

        public Error(string message, int statusCode = -1)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
