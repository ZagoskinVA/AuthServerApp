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
            if(context == null)
                throw new ArgumentNullException(nameof(context));
            _context = context;
        }
        public void AddRefreshTokenToUser(RefreshToken refreshToken, string userId)
        {
            refreshToken.UserId = userId;
            var token = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
            if (token == null)
                _context.RefreshTokens.Add(refreshToken);
            else 
            {
                token.JwtToken = refreshToken.JwtToken;
                token.ExpirationDate = refreshToken.ExpirationDate;
                token.RefreshJwtToken = refreshToken.RefreshJwtToken;
            }
            _context.SaveChanges();
        }

        public RefreshToken GetRefreshTokenByUserId(string userId)
        {
            return _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
