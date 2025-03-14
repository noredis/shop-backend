using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface IProductService
    {
        public Results<Created, BadRequest<string>> Add(Product product, IUrlHelper urlHelper);
        public Results<Ok<List<Product>>, NotFound<string>> FindProducts();
        public Results<Ok<Product>, NotFound<string>> FindProduct(int productId);
    }
}
