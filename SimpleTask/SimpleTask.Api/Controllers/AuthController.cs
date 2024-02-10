using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Services.Interfaces;

namespace SimpleTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _AuthServices;

        public AuthController(IAuthServices authServices)
        {
            _AuthServices = authServices;
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDTo logInDTo)
        {
            var Result = await _AuthServices.LoginAsync(logInDTo);

            return Ok(Result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var Result = await _AuthServices.RegisterAsync(registerDto);

            return Ok(Result);
        }
    }
}