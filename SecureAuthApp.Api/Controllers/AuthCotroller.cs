using Microsoft.AspNetCore.Mvc;
using SecureAuthApp.Core.Interfaces;

namespace SecureAuthApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest request)
        {
            var user = await _authService.RegisterAsync(request.Username, request.Password);
            return Ok(new { Message = "User created successfully!", user.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var token = await _authService.LoginAsync(request.Username, request.Password);

            if (token == "User not found" || token == "Invalid password")
                return Unauthorized(token);

            return Ok(new { Token = token });
        }
    }

    // A simple helper class to receive the JSON data from Postman
    public class AuthRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}