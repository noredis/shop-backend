using Microsoft.AspNetCore.Http.HttpResults;
using shop_backend.Dtos.User;
using shop_backend.Models;
using shop_backend.Validation;

namespace shop_backend.Interfaces.Service
{
    public interface ITokenService
    {
        public void GenerateToken(User user, out string accessToken, out string refreshToken);
        public Result<LogInResponceDto> RefreshAccessToken(string refreshToken);
    }
}
