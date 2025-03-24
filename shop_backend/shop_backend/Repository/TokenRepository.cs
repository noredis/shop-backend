using Microsoft.EntityFrameworkCore;

using shop_backend.Data;
using shop_backend.Interfaces.Repository;
using shop_backend.Models;

namespace shop_backend.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public RefreshToken? FindRefreshToken(string refreshToken)
        {
            return _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefault(t => t.Token == refreshToken);
        }
    }
}
