using AuthServerApp.Models;

namespace AuthServerApp.Interfaces
{
    public interface IJwtTokenGenerator
    {
        public RefreshToken GenerateRefreshToken(User user);
    }
}
