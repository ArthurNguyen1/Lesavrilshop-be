using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Orders;
using lesavrilshop_be.Core.Entities.Orders;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Orders
{
    public interface IShopOrderRepository
    {
        Task<IEnumerable<ShopOrder>> GetAllAsync();
        Task<ShopOrder> GetByIdAsync(int id);
        Task<ShopOrder> CreateAsync(CreateShopOrderDto ShopOrderDto);
        Task UpdateAsync(ShopOrder ShopOrder);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}