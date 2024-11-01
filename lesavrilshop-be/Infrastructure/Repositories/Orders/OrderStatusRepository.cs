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
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllAsync()
        {
            return await _context.OrderStatuses.ToListAsync();
                
        }

        public async Task<OrderStatus> GetByIdAsync(int id)
        {
            return await _context.OrderStatuses.FindAsync(id);
        }

        public async Task<OrderStatus> CreateAsync(CreateOrderStatusDto orderStatusDto)
        {
            var orderStatus = new OrderStatus(
                orderStatusDto.Status
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.OrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();
            
            return orderStatus;
        }

        public async Task UpdateAsync(OrderStatus orderStatus)
        {
            var existingOrderStatus = await _context.OrderStatuses.FindAsync(orderStatus.Id);
            if (existingOrderStatus == null)
                throw new KeyNotFoundException($"OrderStatus with ID {orderStatus.Id} not found");

            existingOrderStatus.Status= orderStatus.Status;

            existingOrderStatus.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
                throw new KeyNotFoundException($"OrderStatus with ID {id} not found");

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.OrderStatuses.AnyAsync(os => os.Id == id);
        }
    }
}