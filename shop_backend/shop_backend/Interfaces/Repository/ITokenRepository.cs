using shop_backend.Models;

namespace shop_backend.Interfaces.Repository
{
    public interface ITokenRepository
    {
        public void AddRefreshToken(RefreshToken refreshToken);
        public RefreshToken? FindRefreshToken(string refreshToken);
    }
}
