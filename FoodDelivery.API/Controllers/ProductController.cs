using FoodDelivery.API.DTOs;
using FoodDelivery.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Add Single Product
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto dto)
        {
            await _productService.AddProduct(dto);

            return Ok(new
            {
                Message = "Product Added Successfully"
            });
        }

        // Add Multiple Products
        [HttpPost("bulk")]
        public async Task<IActionResult> AddProducts(List<ProductDto> products)
        {
            await _productService.AddProducts(products);

            return Ok(new
            {
                Message = "Products Added Successfully"
            });
        }
        // Get All Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
                return NotFound("Product Not Found");

            return Ok(product);
        }

        // Update Product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto dto)
        {
            var updated = await _productService.UpdateProduct(id, dto);

            if (!updated)
                return NotFound("Product Not Found");

            return Ok(new
            {
                Message = "Product Updated Successfully"
            });
        }

        // Delete Product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProduct(id);

            if (!deleted)
                return NotFound("Product Not Found");

            return Ok(new
            {
                Message = "Product Deleted Successfully"
            });
        }
    }
}