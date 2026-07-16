using FoodDelivery.API.Data;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        // Get All Categories
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        // Get Category By Id
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        // Add Category
        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        // Update Category
        public async Task<bool> UpdateCategory(int id, Category category)
        {
            var existing = await _context.Categories.FindAsync(id);

            if (existing == null)
                return false;

            existing.Name = category.Name;
            existing.Description = category.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        // Delete Category
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return false;

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}