using Microsoft.AspNetCore.Http;
using URl_Shortner.DTOs;
using Microsoft.AspNetCore.Mvc;
using URl_Shortner.Models;
using URl_Shortner.Services;

namespace URl_Shortner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;
        public AuthController(IAuthService authservice)
        {
            _authservice = authservice;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest  regrequests)
        {
            var callRegister = await _authservice.Register(regrequests);
            return Ok(callRegister);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest logrequests)
        {
            var LoginUser = await _authservice.Login(logrequests);
            return Ok(LoginUser);

        }
    }
}
