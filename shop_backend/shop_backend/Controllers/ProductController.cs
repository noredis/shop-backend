using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using shop_backend.Dtos.Product;
using shop_backend.Interfaces.Service;
using shop_backend.Mappers;
using shop_backend.Models;
using shop_backend.Validation;

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
            Result<string> responceResult = _productService.AddProduct(productModel, Url);
            
            if (responceResult.IsSuccess)
            {
                return TypedResults.Created(responceResult.Value);
            }
            else
            {
                return TypedResults.BadRequest(responceResult.Error);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("products")]
        public IResult GetProducts()
        {
            Result<List<Product>?> responceResult = _productService.GetProducts();
            
            if (responceResult.IsSuccess)
            {
                return TypedResults.Ok(responceResult.Value);
            }
            else
            {
                return TypedResults.NotFound(responceResult.Error);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("products/{productId}")]
        public IResult GetProduct([FromRoute] int productId)
        {
            Result<Product?> requestResult = _productService.GetProductById(productId);
            
            if (requestResult.IsSuccess)
            {
                return TypedResults.Ok(requestResult.Value);
            }
            else
            {
                return TypedResults.NotFound(requestResult.Error);
            }

        }

        [Authorize]
        [HttpPut]
        [Route("products/{productId}")]
        public IResult PutProduct([FromRoute] int productId, [FromBody] PutProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return TypedResults.BadRequest();
            }

            Result<string> requestResult = _productService.PutProduct(productId, productDto);
            
            if (requestResult.IsSuccess)
            {
                return TypedResults.NoContent();
            }
            else
            {
                return TypedResults.Problem(
                    statusCode: requestResult.Error.StatusCode,
                    detail: requestResult.Error.Message
                );
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("products/{productId}")]
        public IResult PatchProduct([FromRoute] int productId, [FromBody] JsonPatchDocument productDocument)
        {
            Result<string> requestResult = _productService.PatchProduct(productId, productDocument);
            
            if (requestResult.IsSuccess)
            {
                return TypedResults.NoContent();
            }
            else
            {
                return TypedResults.Problem(
                    statusCode: requestResult.Error.StatusCode,
                    detail: requestResult.Error.Message
                );
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("products/{productId}")]
        public IResult DeleteProduct([FromRoute] int productId)
        {
            Result<string> requestResult = _productService.DeleteProduct(productId);
            
            if (requestResult.IsSuccess)
            {
                return TypedResults.NoContent();
            }
            else
            {
                return TypedResults.NotFound(requestResult.Error);
            }
        }
    }
}
