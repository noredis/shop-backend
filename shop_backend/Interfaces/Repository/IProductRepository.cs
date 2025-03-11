using shop_backend.Models;

namespace shop_backend.Interfaces.Repository
{
    public interface IProductRepository
    {
        public void InsertProduct(Product product);
    }
}
