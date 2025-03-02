using shop_backend.Dtos.User;
using shop_backend.Models;

namespace shop_backend.Interfaces.Service
{
    public interface ITokenService
    {
        public void CreateToken(User user, out string accessToken, out string refreshToken);
        public LogInResponceDto RefreshAccessToken(string refreshToken);
    }
}
