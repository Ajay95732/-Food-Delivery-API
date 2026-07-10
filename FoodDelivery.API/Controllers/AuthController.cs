using FoodDelivery.API.DTOs;
using FoodDelivery.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _authService.Register(dto);

            return Ok("User Registered Successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.Login(dto);

            if (user == null)
            {
                return BadRequest("Invalid Email or Password");
            }

            return Ok(new
            {
                Message = "Login Successful",
                User = user
            });
        }
    }
}