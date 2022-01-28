using AuthServerApp.Contexts;
using AuthServerApp.Interfaces;
using AuthServerApp.Models;

namespace AuthServerApp.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationContext _context;

        public RefreshTokenRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void AddRefreshTokenToUser(RefreshToken refreshToken, string userId)
        {
            refreshToken.UserId = userId;
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            refreshToken.User = null;
        }

        public RefreshToken GetRefreshTokenByUserId(string userId)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
