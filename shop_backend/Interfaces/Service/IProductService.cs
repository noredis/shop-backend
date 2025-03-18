using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.Product;
using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface IProductService
    {
        public Results<Created, BadRequest<string>> Add(Product product, IUrlHelper urlHelper);
        public Results<Ok<List<Product>>, NotFound<string>> FindProducts();
        public Results<Ok<Product>, NotFound<string>> FindProduct(int productId);
        public Results<NoContent, NotFound, BadRequest> EditProduct(int id, UpdateProductDto productDto);
        public Results<NoContent, NotFound, BadRequest> EditProduct(int id, JsonPatchDocument productDocument);
        public Results<NoContent, NotFound> RemoveProduct(int id);
    }
}
