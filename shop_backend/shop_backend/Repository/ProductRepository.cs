using Microsoft.AspNetCore.JsonPatch;
using shop_backend.Data;
using shop_backend.Dtos.Product;
using shop_backend.Interfaces.Repository;
using shop_backend.Models;

namespace shop_backend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product? SelectProduct(int productId)
        {
            return _context.Products.Find(productId);
        }

        public List<Product>? SelectProducts()
        {
            return _context.Products.ToList();
        }

        public void UpdateProduct(Product product, PutProductDto productDto)
        {
            product.Name = productDto.Name;
            product.Category = productDto.Category;
            product.Images = productDto.Images;
            product.Description = productDto.Description;
            product.Price = productDto.Price;

            _context.SaveChanges();
        }

        public void UpdateProduct(Product product, JsonPatchDocument productDocument)
        {
            productDocument.ApplyTo(product);

            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
