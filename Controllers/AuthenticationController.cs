using AuthServerApp.Extensions;
using AuthServerApp.Interfaces;
using AuthServerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISignUp _signUpService;
        private readonly ISignIn _signInService;
        public AuthenticationController(ISignIn signInService, ISignUp signUpService)
        {
            _signInService = signInService;
            _signUpService = signUpService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model) 
        {
            if (ModelState.IsValid) 
            {
                var refreshToken = await _signInService.SignInAsync(model);
                if(refreshToken != null)
                    return Ok(refreshToken);
            }
            _signInService.Errors.AddRange(ModelState.GetErrors());
            return BadRequest(_signInService.Errors);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model) 
        {
            if (ModelState.IsValid) 
            {
                var isSucceed = await _signUpService.SignUp(model);
                if (isSucceed) 
                {
                    return Ok();
                }
            }
            _signUpService.Errors.AddRange(ModelState.GetErrors());
            return BadRequest(_signInService.Errors);
        }
    }
}
