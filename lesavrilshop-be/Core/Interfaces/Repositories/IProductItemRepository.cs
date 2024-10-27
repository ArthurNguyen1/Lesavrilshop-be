using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories
{
    public interface IProductItemRepository
    {
        Task<IEnumerable<ProductItem>> GetAllAsync();
        Task<ProductItem> GetByIdAsync(int id);
        Task<ProductItem> CreateAsync(CreateProductItemDto productItemDto);
        Task UpdateAsync(ProductItem productItem);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}