using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.Product;
using shop_backend.Interfaces.Service;
using shop_backend.Mappers;
using shop_backend.Models;

namespace shop_backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpPost]
        [Route("product")]
        public IResult AddProduct([FromBody] AddProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest(ModelState);
            }

            Product productModel = productDto.ToProduct();
            IResult status = _productService.Add(productModel, Url);

            return status;
        }

        [Authorize]
        [HttpGet]
        [Route("products")]
        public IResult GetProducts()
        {
            IResult status = _productService.FindProducts();
            return status;
        }

        [Authorize]
        [HttpGet]
        [Route("products/{productId}")]
        public IResult GetProduct([FromRoute] int productId)
        {
            IResult status = _productService.FindProduct(productId);
            return status;
        }

        [Authorize]
        [HttpPut]
        [Route("products/{productId}")]
        public IResult UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest();
            }

            IResult status = _productService.EditProduct(productId, productDto);
            return status;
        }
    }
}
