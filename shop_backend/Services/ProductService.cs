using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Models;

namespace shop_backend.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public Results<Created, BadRequest<string>> Add(Product product, IUrlHelper urlHelper)
        {
            if (product.Price <= 0d)
            {
                return TypedResults.BadRequest("Product price must be greater than zero");
            }
            else
            {
                _productRepo.InsertProduct(product);
                return TypedResults.Created(urlHelper.Action());
            }
        }
    }
}
