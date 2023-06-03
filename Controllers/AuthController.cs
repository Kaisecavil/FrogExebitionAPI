using FrogExebitionAPI.Models;
using FrogExebitionAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrogExebitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) 
        {
            _authService = authService;
        } 
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            if(await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
                //return Ok("Successful login");
            }
            return BadRequest("Wrong username or password");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginUser user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok("Successful registration");
            }
            return BadRequest("smth went wrong");
            //return Ok(await _authService.RegisterUser(user));
        }
    }
}
