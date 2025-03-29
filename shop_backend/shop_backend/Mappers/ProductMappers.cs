using shop_backend.Dtos.Product;
using shop_backend.Models;

namespace shop_backend.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProduct(this AddProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Category = productDto.Category,
                Images = productDto.Images,
                Description = productDto.Description,
                Price = productDto.Price
            };
        }
    }
}
