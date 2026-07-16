using Microsoft.AspNetCore.Http;

namespace FoodDelivery.API.DTOs
{
    public class UploadImageDto
    {
        public IFormFile File { get; set; }
    }
}
