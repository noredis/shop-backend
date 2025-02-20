using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.User
{
    public class UserResponceDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string FullName { get; set; } = String.Empty;
        [Required]
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
