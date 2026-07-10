using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Users Table
        public DbSet<User> Users { get; set; }

        // Products Table
        public DbSet<Product> Products { get; set; }
    }
}