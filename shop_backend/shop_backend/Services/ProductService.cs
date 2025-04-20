using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

using System.Buffers.Text;

using SixLabors.ImageSharp;

using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Models;
using shop_backend.Dtos.Product;
using shop_backend.Validation;
using shop_backend.Validation.Product;

namespace shop_backend.Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public Result<string> AddProduct(Product product, IUrlHelper urlHelper)
        {
            if (product.Images != null)
            {
                List<ProductImage> images = new List<ProductImage>();
                int imageCounter = 1;

                foreach (ProductImage byteImage in product.Images.ToList())
                {
                    if (!Base64.IsValid(byteImage.Path))
                    {
                        return Result<string>.Failure(new Error("Image file is corrupted"));
                    }

                    byte[] imageData = Convert.FromBase64String(byteImage.Path);
                    Image image = Image.Load(imageData);

                    DateTime now = DateTime.UtcNow.AddHours(6);
                    string currentDate = now.ToString("dd-MM-yyyy");
                    string currentTime = now.ToString("HH-mm-ss");

                    string imagePath = $"/home/downloads/shop-backend/image_{currentDate}_{currentTime}_#{imageCounter}.png";
                    imageCounter++;

                    image.Save(imagePath);

                    images.Add(new ProductImage { Path = imagePath });
                }

                product.Images.Clear();

                foreach (ProductImage image in images)
                {
                    product.Images.Add(image);
                }
            }

            if (product.Price <= 0d)
            {
                return Result<string>.Failure(new Error("Product price must be greater than zero"));
            }
            else
            {
                _productRepo.AddProduct(product);

                return Result<string>.Success(urlHelper.Action());
            }
        }

        public Result<Product?> GetProductById(int productId)
        {
            Product? product = _productRepo.GetProduct(productId);

            if (product == null)
            {
                return Result<Product?>.Failure(new Error("Product not found"));
            }

            return Result<Product?>.Success(product);
        }

        public Result<List<Product>?> GetProducts()
        {
            List<Product>? products = _productRepo.GetProducts();

            if (products == null)
            {
                return Result<List<Product>?>.Failure(new Error("There are no products avalable in the shop"));
            }
            else
            {
                return Result<List<Product>?>.Success(products);
            }
        }

        public Result<string> PutProduct(int id, PutProductDto productDto)
        {
            Product? product = _productRepo.GetProduct(id);

            if (product == null)
            {
                return Result<string>.Failure(new Error("Product not found", 404));
            }

            if (productDto.Price <= 0d)
            {
                return Result<string>.Failure(new Error("Product price must be greater than zero", 400));
            }

            _productRepo.UpdateProduct(product, productDto);
            return Result<string>.Success(String.Empty);
        }
        public Result<string> PatchProduct(int id, JsonPatchDocument productDocument)
        {
            Result<JsonPatchDocument> validationResult = ProductValidator.ValidatePatch(productDocument);

            if (!validationResult.IsSuccess)
            {
                return Result<string>.Failure(new Error(validationResult.Error.Message, 400));
            }

            Product? product = _productRepo.GetProduct(id);

            if (product == null)
            {
                return Result<string>.Failure(new Error("Product not found", 404));
            }

            _productRepo.UpdateProductPartially(product, productDocument);
            return Result<string>.Success(String.Empty);
        }
        public Result<string> DeleteProduct(int id)
        {
            Product? product = _productRepo.GetProduct(id);

            if (product == null)
            {
                return Result<string>.Failure(new Error("Product not found"));
            }

            _productRepo.DeleteProduct(product);
            return Result<string>.Success(String.Empty);
        }
    }
}
