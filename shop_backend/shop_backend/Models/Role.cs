using Microsoft.AspNetCore.Identity;

namespace shop_backend.Models
{
    public class Role : IdentityRole<int>
    {
        public string? Description { get; set; }
    }
}
