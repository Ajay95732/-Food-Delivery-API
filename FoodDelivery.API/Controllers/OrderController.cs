using FoodDelivery.API.DTOs;
using FoodDelivery.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Add Order
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDto dto)
        {
            await _orderService.AddOrder(dto);

            return Ok(new
            {
                Message = "Order Placed Successfully"
            });
        }

        // Get All Orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();

            return Ok(orders);
        }

        // Get Orders By Phone
        [HttpGet("customer/{phone}")]
        public async Task<IActionResult> GetOrdersByPhone(string phone)
        {
            var orders = await _orderService.GetOrdersByPhone(phone);

            return Ok(orders);
        }

        // Get Order By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
                return NotFound("Order Not Found");

            return Ok(order);
        }

        // Update Order Status
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var updated = await _orderService.UpdateOrderStatus(id, status);

            if (!updated)
                return NotFound("Order Not Found");

            return Ok(new
            {
                Message = "Order Updated Successfully"
            });
        }

        // Delete Order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrder(id);

            if (!deleted)
                return NotFound("Order Not Found");

            return Ok(new
            {
                Message = "Order Deleted Successfully"
            });
        }
    }
}