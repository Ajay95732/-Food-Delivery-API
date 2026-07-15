namespace FoodDelivery.API.DTOs
{
    public class OrderDto
    {
        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItemDto> Items { get; set; }
            = new List<OrderItemDto>();
    }
}