using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Orders;
using lesavrilshop_be.Core.Entities.Orders;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Orders
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllAsync();
        Task<OrderStatus> GetByIdAsync(int id);
        Task<OrderStatus> CreateAsync(CreateOrderStatusDto orderStatusDto);
        Task UpdateAsync(OrderStatus orderStatus);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}