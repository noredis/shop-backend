using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.User
{
    public class RegisterUserRequestDto
    {
        public string Email { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        [MinLength(6, ErrorMessage = "Password should be at least 6 characters length")]
        public string Password { get; set; } = String.Empty;
        public string PasswordConfirm { get; set; } = String.Empty;
    }
}
