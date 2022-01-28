using AuthServerApp.Models;

namespace AuthServerApp.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void AddRefreshTokenToUser(RefreshToken refreshToken, string userId);
        RefreshToken GetRefreshTokenByUserId(string userId);
    }
}
