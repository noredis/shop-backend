using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using System.Buffers.Text;

using SixLabors.ImageSharp;

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
    }
}
