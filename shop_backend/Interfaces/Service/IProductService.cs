using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface IProductService
    {
        public Results<Created, BadRequest<string>> Add(Product product, IUrlHelper urlHelper);
    }
}
