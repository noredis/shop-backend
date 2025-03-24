using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.User
{
    public class LogInUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
