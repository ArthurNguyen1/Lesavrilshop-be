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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
                
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> CreateAsync(CreateOrderItemDto orderItemDto)
        {
            var orderItem = new OrderItem(
                orderItemDto.ProductItemId,
                orderItemDto.OrderId,
                orderItemDto.Price,
                orderItemDto.Quantity
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            
            return orderItem;
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.Id);
            if (existingOrderItem == null)
                throw new KeyNotFoundException($"OrderItem with ID {orderItem.Id} not found");

            existingOrderItem.ProductItemId= orderItem.ProductItemId;
            existingOrderItem.OrderId= orderItem.OrderId;
            existingOrderItem.Price= orderItem.Price;
            existingOrderItem.Quantity= orderItem.Quantity;

            existingOrderItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
                throw new KeyNotFoundException($"OrderItem with ID {id} not found");

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.OrderItems.AnyAsync(oi => oi.Id == id);
        }
    }
}