using FoodDelivery.API.Models;
using FoodDelivery.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Get All Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        // Get Category By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound("Category Not Found");

            return Ok(category);
        }

        // Add Category
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _categoryService.AddCategory(category);

            return Ok(new
            {
                Message = "Category Added Successfully"
            });
        }

        // Update Category
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            var updated = await _categoryService.UpdateCategory(id, category);

            if (!updated)
                return NotFound("Category Not Found");

            return Ok(new
            {
                Message = "Category Updated Successfully"
            });
        }

        // Delete Category
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategory(id);

            if (!deleted)
                return NotFound("Category Not Found");

            return Ok(new
            {
                Message = "Category Deleted Successfully"
            });
        }
    }
}