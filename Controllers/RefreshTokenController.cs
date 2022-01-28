using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private readonly IRefreshTokenManager _refreshTokenManager;
        public RefreshTokenController(IRefreshTokenManager refreshTokenManager)
        {
            if(refreshTokenManager == null)
                throw new ArgumentNullException(nameof(refreshTokenManager));
            _refreshTokenManager = refreshTokenManager;
        }
        [HttpPost("UpdateRefreshToken")]
        public IActionResult UpdateRefreshToken(RefreshToken token)
        {
            if (token.User == null || string.IsNullOrEmpty(token.UserId))
                return BadRequest("Не указан пользователь");
            var isValid = _refreshTokenManager.IsValid(token);
            if (isValid) 
            {
                _refreshTokenManager.UpdateRefreshToken(token);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
