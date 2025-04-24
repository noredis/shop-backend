using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop_backend.Models;

namespace shop_backend.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Admin role"
                },
                new Role
                {
                    Id = 2,
                    Name = "Seller",
                    NormalizedName = "SELLER",
                    Description = "Seller role"
                },
                new Role
                {
                    Id = 3,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    Description = "Customer role"
                }
            );
        }
    }
}
