using AuthServerApp.Models;

namespace AuthServerApp.Interfaces
{
    public interface IRefreshTokenManager
    {
        bool IsValid(RefreshToken refreshToken);
        RefreshToken GetNewRefreshToken(RefreshToken oldRefreshToken);
        void UpdateRefreshToken(RefreshToken refreshToken);
    }
}
