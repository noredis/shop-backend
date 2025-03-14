using shop_backend.Data;
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
    }
}
