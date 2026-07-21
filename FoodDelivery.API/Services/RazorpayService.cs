using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.API.Services
{
    public class RazorpayService
    {
        private readonly string keyId;
        private readonly string secret;


        public RazorpayService(IConfiguration configuration)
        {
            keyId = configuration["Razorpay:KeyId"]
                ?? throw new Exception("Razorpay KeyId missing in appsettings.json");


            secret = configuration["Razorpay:Secret"]
                ?? throw new Exception("Razorpay Secret missing in appsettings.json");
        }


        // CREATE RAZORPAY ORDER
        public object CreateOrder(decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    throw new Exception("Invalid amount");
                }


                RazorpayClient client =
                    new RazorpayClient(
                        keyId,
                        secret
                    );


                Dictionary<string, object> options =
                    new Dictionary<string, object>();


                options.Add(
                    "amount",
                    Convert.ToInt32(amount * 100)
                );


                options.Add(
                    "currency",
                    "INR"
                );


                options.Add(
                    "receipt",
                    "order_" + Guid.NewGuid()
                        .ToString("N")
                        .Substring(0, 20)
                );


                Razorpay.Api.Order order =
                    client.Order.Create(options);


                return new
                {
                    id = order["id"].ToString(),
                    entity = order["entity"].ToString(),
                    amount = Convert.ToInt32(order["amount"]),
                    amount_paid = Convert.ToInt32(order["amount_paid"]),
                    amount_due = Convert.ToInt32(order["amount_due"]),
                    currency = order["currency"].ToString(),
                    status = order["status"].ToString(),
                    receipt = order["receipt"].ToString()
                };

            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Razorpay Order Creation Failed: "
                    + ex.Message
                );
            }
        }





        // VERIFY PAYMENT
        public bool VerifyPayment(
            string razorpayOrderId,
            string razorpayPaymentId,
            string razorpaySignature
        )
        {

            if (string.IsNullOrEmpty(razorpayOrderId) ||
               string.IsNullOrEmpty(razorpayPaymentId) ||
               string.IsNullOrEmpty(razorpaySignature))
            {
                return false;
            }



            string payload =
                razorpayOrderId
                +
                "|"
                +
                razorpayPaymentId;



            using HMACSHA256 hmac =
                new HMACSHA256(
                    Encoding.UTF8.GetBytes(secret)
                );



            byte[] hash =
                hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(payload)
                );



            string generatedSignature =
                BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLower();



            return generatedSignature
                    ==
                   razorpaySignature;

        }
    }
}