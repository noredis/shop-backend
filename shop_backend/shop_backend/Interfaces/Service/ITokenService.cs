using shop_backend.Dtos.User;
using shop_backend.Validation;

namespace shop_backend.Interfaces.Service
{
    public interface ITokenService
    {
        public Result<LogInResponceDto> RefreshAccessToken(string refreshToken);
        public string GenerateRefreshToken();
    }
}
