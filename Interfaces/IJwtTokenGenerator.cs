using AuthServerApp.Models;

namespace AuthServerApp.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public RefreshToken GenerateRefreshToken(UserViewModel user);
        public void UpdatRefreshToken(RefreshToken refreshToken);
    }
}
