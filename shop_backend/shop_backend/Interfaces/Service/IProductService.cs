using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.Product;
using shop_backend.Models;
using shop_backend.Validation;

namespace shop_backend.Interfaces.Service
{
    public interface IProductService
    {
        public Result<string> AddProduct(Product product, IUrlHelper urlHelper);
        public Result<List<Product>?> GetProducts();
        public Result<Product?> GetProductById(int productId);
        public Result<string> PutProduct(int id, PutProductDto productDto);
        public Result<string> PatchProduct(int id, JsonPatchDocument productDocument);
        public Result<string> DeleteProduct(int id);
    }
}
