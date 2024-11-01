using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Orders;
using lesavrilshop_be.Core.Entities.Orders;
using lesavrilshop_be.Core.Interfaces.Repositories.Orders;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Orders
{
    public class ShippingMethodRepository : IShippingMethodRepository
    {
        private readonly ApplicationDbContext _context;

        public ShippingMethodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShippingMethod>> GetAllAsync()
        {
            return await _context.ShippingMethods.ToListAsync();
                
        }

        public async Task<ShippingMethod> GetByIdAsync(int id)
        {
            return await _context.ShippingMethods.FindAsync(id);
        }

        public async Task<ShippingMethod> CreateAsync(CreateShippingMethodDto ShippingMethodDto)
        {
            var ShippingMethod = new ShippingMethod(
                ShippingMethodDto.Name,
                ShippingMethodDto.Description,
                ShippingMethodDto.Price
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.ShippingMethods.Add(ShippingMethod);
            await _context.SaveChangesAsync();
            
            return ShippingMethod;
        }

        public async Task UpdateAsync(ShippingMethod ShippingMethod)
        {
            var existingShippingMethod = await _context.ShippingMethods.FindAsync(ShippingMethod.Id);
            if (existingShippingMethod == null)
                throw new KeyNotFoundException($"ShippingMethod with ID {ShippingMethod.Id} not found");

            existingShippingMethod.Name= ShippingMethod.Name;
            existingShippingMethod.Description= ShippingMethod.Description;
            existingShippingMethod.Price= ShippingMethod.Price;

            existingShippingMethod.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ShippingMethod = await _context.ShippingMethods.FindAsync(id);
            if (ShippingMethod == null)
                throw new KeyNotFoundException($"ShippingMethod with ID {id} not found");

            _context.ShippingMethods.Remove(ShippingMethod);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ShippingMethods.AnyAsync(sm => sm.Id == id);
        }
    }
}