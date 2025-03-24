using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.RefreshToken
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
