using System.Linq;
using FoodDelivery.API.Data;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // Add Single Product
        public async Task AddProduct(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Add Multiple Products
        public async Task AddProducts(List<ProductDto> dtos)
        {
            var products = dtos.Select(dto => new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category
            }).ToList();

            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }

        // Get All Products
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // Get Product By Id
        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Update Product
        public async Task<bool> UpdateProduct(int id, ProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.Category = dto.Category;

            await _context.SaveChangesAsync();

            return true;
        }

        // Delete Product
        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}