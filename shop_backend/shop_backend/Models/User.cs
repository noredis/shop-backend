using Microsoft.AspNetCore.Identity;

namespace shop_backend.Models
{
    public class User : IdentityUser<int>
    {
        public override int Id { get; set; }
        public override string Email { get; set; } = String.Empty;
        public string FullName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
