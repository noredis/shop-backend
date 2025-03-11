using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using shop_backend.Models;

namespace shop_backend.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Token).HasMaxLength(200);

            builder.HasIndex(t => t.Token).IsUnique();

            builder.HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserID);
        }
    }
}
