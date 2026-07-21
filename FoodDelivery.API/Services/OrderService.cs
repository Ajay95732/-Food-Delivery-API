using FoodDelivery.API.Data;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        // Add Order
        public async Task AddOrder(OrderDto dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                Phone = dto.Phone,
                Address = dto.Address,
                TotalAmount = dto.TotalAmount,
                Status = "Pending",
                OrderDate = DateTime.Now
            };

            foreach (var item in dto.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
        }

        // Get All Orders
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // Get Customer Orders
        public async Task<List<Order>> GetOrdersByPhone(string phone)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.Phone == phone)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // Get Order By Id
        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // Update Order Status
        public async Task<bool> UpdateOrderStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            order.Status = status;

            await _context.SaveChangesAsync();

            return true;
        }

        // Delete Order
        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return false;

            _context.OrderItems.RemoveRange(order.OrderItems);
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}