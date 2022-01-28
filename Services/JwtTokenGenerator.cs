using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthServerApp.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly SymmetricSecurityKey _key;
        private IConfiguration _config;

        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((config["TokenKey"])));
        }
        public RefreshToken GenerateRefreshToken(UserViewModel user)
        {
            var token =  new RefreshToken { JwtToken = GenerateJwtToken(user), RefreshJwtToken = GenerateRefreshJwtToken(), UserId = user.Id,  User = user};
            token.UpdateExpDate();
            return token;
        }

        public void UpdatRefreshToken(RefreshToken refreshToken)
        {
            refreshToken.JwtToken = GenerateJwtToken(refreshToken.User);
            refreshToken.UpdateExpDate();
        }

        private string GenerateJwtToken(UserViewModel user) 
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Name", user.NickName)
            };
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor { Subject = new ClaimsIdentity(claims), Expires = DateTime.Now.AddDays(7), SigningCredentials = credentials };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshJwtToken()
        {
            using (var hash = SHA512.Create())
            {
                var key = Guid.NewGuid().ToString();
                var hashedInputBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
