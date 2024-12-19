using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Products
{
    public interface IProductItemRepository
    {
        Task<IEnumerable<ProductItem>> GetByProductIdAsync(int productId);
        Task<ProductItem?> GetByIdAsync(int id);
        Task<ProductItem> CreateAsync(ProductItem productItem);
        Task UpdateAsync(ProductItem productItem);
        Task DeleteAsync(int id);
    }
}