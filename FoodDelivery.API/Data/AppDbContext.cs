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

        // Orders Table
        public DbSet<Order> Orders { get; set; }

        // Order Items Table
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}