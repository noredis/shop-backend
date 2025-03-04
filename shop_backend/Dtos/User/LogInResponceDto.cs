using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.User
{
    public class LogInResponceDto
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
