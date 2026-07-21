using FoodDelivery.API.DTOs;
using FoodDelivery.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {


        private readonly RazorpayService _razorpayService;



        public PaymentController(
            RazorpayService razorpayService
        )
        {
            _razorpayService = razorpayService;
        }





        [HttpPost("create-order")]
        public IActionResult CreateOrder(
    [FromBody] CreatePaymentDto dto
)
        {

            var order =
                _razorpayService
                .CreateOrder(dto.Amount);



            return Ok(order);

        }






        [HttpPost("verify")]
        public IActionResult VerifyPayment(
            VerifyPaymentDto dto
        )
        {


            bool result =
                _razorpayService.VerifyPayment(
                    dto.RazorpayOrderId,
                    dto.RazorpayPaymentId,
                    dto.RazorpaySignature
                );



            if (!result)
            {
                return BadRequest(
                    "Payment Verification Failed"
                );
            }



            return Ok(new
            {
                Message =
                "Payment Successful"
            });

        }

    }
}