using Microsoft.EntityFrameworkCore;
using shop_backend.Models;

namespace shop_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> User { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
