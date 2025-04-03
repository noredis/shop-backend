using shop_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace shop_backend.Dtos.Product
{
    public class AddProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        public IList<ProductImage>? Images { get; set; }
        public string? Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
    }
}
