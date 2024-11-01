using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Products
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(CreateProductCategoryDto productCategoryDto);
        Task UpdateAsync(ProductCategory productCategory);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}