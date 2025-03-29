using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using shop_backend.Models;

namespace shop_backend.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.ToTable(p => p.HasCheckConstraint("CK_Product_Price", "\"Price\" > 0"));

            builder.Property(p => p.Images).HasConversion(
                j => JsonConvert.SerializeObject(j, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                j => JsonConvert.DeserializeObject<IList<ProductImage>>(j, new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
