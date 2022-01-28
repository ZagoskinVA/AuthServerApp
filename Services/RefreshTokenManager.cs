using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using System.Globalization;

namespace AuthServerApp.Services
{
    public class RefreshTokenManager : IRefreshTokenManager
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
        public RefreshTokenManager(IRefreshTokenRepository refreshTokenRepository, IJwtTokenGenerator tokenGenerator)
        {
            if(refreshTokenRepository == null)
                throw new ArgumentNullException(nameof(refreshTokenRepository));
            if(tokenGenerator == null)
                throw new ArgumentNullException(nameof(tokenGenerator));
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
        }
        public RefreshToken GetNewRefreshToken(RefreshToken oldRefreshToken)
        {
            var token = _tokenGenerator.GenerateRefreshToken(oldRefreshToken.User);
            _refreshTokenRepository.AddRefreshTokenToUser(oldRefreshToken, oldRefreshToken.UserId);
            return token;
        }



        public bool IsValid(RefreshToken refreshToken)
        {
            var token = _refreshTokenRepository.GetRefreshTokenByUserId(refreshToken.UserId);
            if (token == null)
                throw new Exception("Пользователя не существует");
            return  refreshToken.RefreshJwtToken == token.RefreshJwtToken && DateTime.ParseExact(token.ExpirationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= DateTime.UtcNow;
        }

        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            _tokenGenerator.UpdatRefreshToken(refreshToken);
            _refreshTokenRepository.AddRefreshTokenToUser(refreshToken, refreshToken.UserId);
        }
    }
}
