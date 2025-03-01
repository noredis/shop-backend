using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
