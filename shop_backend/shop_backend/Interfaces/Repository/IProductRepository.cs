using Microsoft.AspNetCore.JsonPatch;
using shop_backend.Dtos.Product;
using shop_backend.Models;

namespace shop_backend.Interfaces.Repository
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);
        public List<Product>? GetProducts();
        public Product? GetProduct(int productId);
        public void UpdateProduct(Product product, PutProductDto productDto);
        public void UpdateProductPartially(Product product, JsonPatchDocument productDocument);
        public void DeleteProduct(Product product);
    }
}
