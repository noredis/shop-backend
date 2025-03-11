using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

using shop_backend.Dtos.User;
using shop_backend.Interfaces.Repository;
using shop_backend.Interfaces.Service;
using shop_backend.Models;


namespace shop_backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly ITokenRepository _tokenRepo;

        public TokenService(IConfiguration config, ITokenRepository tokenRepo)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));

            _tokenRepo = tokenRepo;
        }

        public void CreateToken(User user, out string accessToken, out string refreshToken)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(24),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            RefreshToken _refreshToken = new RefreshToken
            {
                UserID = user.Id,
                Token = GenerateRefreshToken()
            };

            _tokenRepo.InsertRefreshToken(_refreshToken);

            accessToken = tokenHandler.WriteToken(token);
            refreshToken = _refreshToken.Token;
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        public Results<Ok<LogInResponceDto>, UnauthorizedHttpResult> RefreshAccessToken(string refreshToken)
        {
            RefreshToken? token = _tokenRepo.FindRefreshToken(refreshToken);

            string newAccessToken = String.Empty;
            string newRefreshToken = String.Empty;

            if (token != null)
            {
                CreateToken(token.User, out newAccessToken, out newRefreshToken);
                return TypedResults.Ok(
                    new LogInResponceDto
                    {
                        AccessToken = newAccessToken,
                        RefreshToken = newRefreshToken
                    }
                );
            }
            else
            {
                return TypedResults.Unauthorized();
            }
        }
    }
}
