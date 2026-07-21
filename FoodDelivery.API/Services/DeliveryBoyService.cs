using FoodDelivery.API.Data;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Services
{
    public class DeliveryBoyService
    {
        private readonly AppDbContext _context;

        public DeliveryBoyService(AppDbContext context)
        {
            _context = context;
        }


        // Get All Delivery Boys
        public async Task<List<DeliveryBoyDto>> GetAll()
        {
            return await _context.DeliveryBoys
                .Select(d => new DeliveryBoyDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Phone = d.Phone,
                    Email = d.Email,
                    IsActive = d.IsActive
                })
                .ToListAsync();
        }



        // Add Delivery Boy
        public async Task Add(CreateDeliveryBoyDto dto)
        {

            var deliveryBoy = new DeliveryBoy
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Password = dto.Password,
                IsActive = true
            };


            _context.DeliveryBoys.Add(deliveryBoy);

            await _context.SaveChangesAsync();

        }



        // Get By Id
        public async Task<DeliveryBoy?> GetById(int id)
        {
            return await _context.DeliveryBoys
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        // Update
        public async Task<bool> Update(
            int id,
            CreateDeliveryBoyDto dto)
        {

            var deliveryBoy =
                await _context.DeliveryBoys
                .FindAsync(id);


            if (deliveryBoy == null)
                return false;


            deliveryBoy.Name = dto.Name;
            deliveryBoy.Phone = dto.Phone;
            deliveryBoy.Email = dto.Email;


            if (!string.IsNullOrEmpty(dto.Password))
            {
                deliveryBoy.Password = dto.Password;
            }


            await _context.SaveChangesAsync();


            return true;

        }



        // Delete
        public async Task<bool> Delete(int id)
        {

            var deliveryBoy =
                await _context.DeliveryBoys
                .FindAsync(id);


            if (deliveryBoy == null)
                return false;


            _context.DeliveryBoys.Remove(deliveryBoy);


            await _context.SaveChangesAsync();


            return true;

        }


    }
}