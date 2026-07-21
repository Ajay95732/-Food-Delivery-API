using FoodDelivery.API.DTOs;
using FoodDelivery.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryBoyController : ControllerBase
    {
        private readonly DeliveryBoyService _service;


        public DeliveryBoyController(
            DeliveryBoyService service)
        {
            _service = service;
        }



        // GET: api/DeliveryBoy
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAll();

            return Ok(data);
        }




        // GET: api/DeliveryBoy/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var deliveryBoy =
                await _service.GetById(id);


            if (deliveryBoy == null)
                return NotFound("Delivery Boy Not Found");


            return Ok(deliveryBoy);
        }





        // POST: api/DeliveryBoy
        [HttpPost]
        public async Task<IActionResult> Add(
            CreateDeliveryBoyDto dto)
        {

            await _service.Add(dto);


            return Ok(new
            {
                Message = "Delivery Boy Added Successfully"
            });

        }





        // PUT: api/DeliveryBoy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CreateDeliveryBoyDto dto)
        {

            var result =
                await _service.Update(id, dto);


            if (!result)
                return NotFound("Delivery Boy Not Found");


            return Ok(new
            {
                Message = "Updated Successfully"
            });

        }





        // DELETE: api/DeliveryBoy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {

            var result =
                await _service.Delete(id);


            if (!result)
                return NotFound("Delivery Boy Not Found");


            return Ok(new
            {
                Message = "Deleted Successfully"
            });

        }

    }
}