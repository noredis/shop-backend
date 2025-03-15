using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System.Buffers.Text;

using SixLabors.ImageSharp;

using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Models;
using shop_backend.Dtos.Product;
using Microsoft.AspNetCore.JsonPatch;

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
            if (product.Image != null)
            {
                if (!Base64.IsValid(product.Image))
                {
                    return TypedResults.BadRequest("Image file is corrupted");
                }

                byte[] imageData = Convert.FromBase64String(product.Image);
                Image image = Image.Load(imageData);

                DateTime now = DateTime.UtcNow.AddHours(6);
                string currentDate = now.ToString("d");
                string currentTime = now.ToString("HH-mm-ss");

                string imagePath = @$"C:\VSProjects\Downloads\Shop-Backend\image_{currentDate}_{currentTime}.png";

                image.Save(imagePath);

                product.Image = imagePath;
            }

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

        public Results<Ok<Product>, NotFound<string>> FindProduct(int productId)
        {
            Product? product = _productRepo.SelectProduct(productId);

            if (product == null)
            {
                return TypedResults.NotFound("The specified product does not exist");
            }

            return TypedResults.Ok(product);
        }

        public Results<Ok<List<Product>>, NotFound<string>> FindProducts()
        {
            List<Product>? products = _productRepo.SelectProducts();

            if (products == null)
            {
                return TypedResults.NotFound("There are no products avalable in the shop");
            }

            return TypedResults.Ok(products);
        }

        public Results<NoContent, NotFound, BadRequest> EditProduct(int id, UpdateProductDto productDto)
        {
            Product? product = _productRepo.SelectProduct(id);

            if (product == null)
            {
                return TypedResults.NotFound();
            }

            if (productDto.Price <= 0d)
            {
                return TypedResults.BadRequest();
            }

            _productRepo.UpdateProduct(product, productDto);
            return TypedResults.NoContent();
        }

        public Results<NoContent, NotFound, BadRequest> EditProduct(int id, JsonPatchDocument productDocument)
        {
            Product? product = _productRepo.SelectProduct(id);

            if (product == null)
            {
                return TypedResults.NotFound();
            }

            _productRepo.UpdateProduct(product, productDocument);
            return TypedResults.NoContent();
        }
    }
}
