namespace FoodDelivery.API.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string RazorpayOrderId { get; set; }

        public string RazorpayPaymentId { get; set; }

        public string RazorpaySignature { get; set; }

        public string Status { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}