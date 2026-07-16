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

        // Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _authService.Register(dto);

            return Ok(new
            {
                Message = "User Registered Successfully"
            });
        }

        // Login
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

        // Get All Users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsers();

            return Ok(users);
        }

        // Update User
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, RegisterDto dto)
        {
            var updated = await _authService.UpdateUser(id, dto);

            if (!updated)
            {
                return NotFound("User Not Found");
            }

            return Ok(new
            {
                Message = "User Updated Successfully"
            });
        }

        // Delete User
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _authService.DeleteUser(id);

            if (!deleted)
            {
                return NotFound("User Not Found");
            }

            return Ok(new
            {
                Message = "User Deleted Successfully"
            });
        }
    }
}