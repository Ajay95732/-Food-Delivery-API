namespace FoodDelivery.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ICollection<OrderItem> OrderItems { get; set; }
    = new List<OrderItem>();
    }
}