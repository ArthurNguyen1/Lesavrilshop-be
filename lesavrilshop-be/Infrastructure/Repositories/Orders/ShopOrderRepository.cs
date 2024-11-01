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
    public class ShopOrderRepository : IShopOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShopOrder>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
                
        }

        public async Task<ShopOrder> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ShopOrder> CreateAsync(CreateShopOrderDto ShopOrderDto)
        {
            var ShopOrder = new ShopOrder(
                ShopOrderDto.UserId,
                ShopOrderDto.PaymentMethod,
                ShopOrderDto.ShippingMethodId,
                ShopOrderDto.ShippingAddressId,
                ShopOrderDto.Note
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.Orders.Add(ShopOrder);
            await _context.SaveChangesAsync();
            
            return ShopOrder;
        }

        public async Task UpdateAsync(ShopOrder ShopOrder)
        {
            var existingShopOrder = await _context.Orders.FindAsync(ShopOrder.Id);
            if (existingShopOrder == null)
                throw new KeyNotFoundException($"ShopOrder with ID {ShopOrder.Id} not found");

            existingShopOrder.UserId= ShopOrder.UserId;
            existingShopOrder.OrderStatusId= ShopOrder.OrderStatusId;
            existingShopOrder.PaymentMethod= ShopOrder.PaymentMethod;
            existingShopOrder.CouponId= ShopOrder.CouponId;

            existingShopOrder.ShippingMethodId= ShopOrder.ShippingMethodId;
            existingShopOrder.ShippingAddressId= ShopOrder.ShippingAddressId;
            existingShopOrder.TotalQuantity= ShopOrder.TotalQuantity;
            existingShopOrder.Note= ShopOrder.Note;

            existingShopOrder.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ShopOrder = await _context.Orders.FindAsync(id);
            if (ShopOrder == null)
                throw new KeyNotFoundException($"ShopOrder with ID {id} not found");

            _context.Orders.Remove(ShopOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }
    }
}