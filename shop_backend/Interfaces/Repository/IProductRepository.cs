using Microsoft.AspNetCore.JsonPatch;
using shop_backend.Dtos.Product;
using shop_backend.Models;

namespace shop_backend.Interfaces.Repository
{
    public interface IProductRepository
    {
        public void InsertProduct(Product product);
        public List<Product>? SelectProducts();
        public Product? SelectProduct(int productId);
        public void UpdateProduct(Product product, UpdateProductDto productDto);
        public void UpdateProduct(Product product, JsonPatchDocument productDocument);
    }
}
