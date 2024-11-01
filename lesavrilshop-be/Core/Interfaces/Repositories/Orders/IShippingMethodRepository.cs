using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Orders;
using lesavrilshop_be.Core.Entities.Orders;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Orders
{
    public interface IShippingMethodRepository
    {
        Task<IEnumerable<ShippingMethod>> GetAllAsync();
        Task<ShippingMethod> GetByIdAsync(int id);
        Task<ShippingMethod> CreateAsync(CreateShippingMethodDto ShippingMethodDto);
        Task UpdateAsync(ShippingMethod ShippingMethod);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}