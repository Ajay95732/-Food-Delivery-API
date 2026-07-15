using FoodDelivery.API.Data;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        // Register User
        public async Task Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = "User"
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        // Login User
        public async Task<User?> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return null;

            if (user.PasswordHash != dto.Password)
                return null;

            return user;
        }

        // Get All Users
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .OrderByDescending(u => u.Id)
                .ToListAsync();
        }

        // Delete User
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}