using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using shop_backend.Models;

namespace shop_backend.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable(p => p.HasCheckConstraint("CK_Product_Price", "\"Price\" > 0"));
        }
    }
}
