using Microsoft.AspNetCore.Identity;

namespace shop_backend.Validation
{
    public class Error
    {
        public IEnumerable<IdentityError> Errors { get; set; }
        public string Message { get; }
        public int StatusCode { get; }

        public Error(string message, int statusCode = -1)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public Error (IEnumerable<IdentityError> errors)
        {
            Errors = errors;
            Message = String.Empty;
            StatusCode = default;
        }
    }
}
