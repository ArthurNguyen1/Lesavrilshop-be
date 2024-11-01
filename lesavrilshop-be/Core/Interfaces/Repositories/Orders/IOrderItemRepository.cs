using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Orders;
using lesavrilshop_be.Core.Entities.Orders;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Orders
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem> GetByIdAsync(int id);
        Task<OrderItem> CreateAsync(CreateOrderItemDto orderItemDto);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}