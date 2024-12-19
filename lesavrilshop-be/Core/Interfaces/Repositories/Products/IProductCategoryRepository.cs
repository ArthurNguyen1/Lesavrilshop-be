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
        Task<IEnumerable<ProductCategory>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(int categoryId);
        Task<bool> ExistsAsync(int productId, int categoryId);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task CreateRangeAsync(IEnumerable<ProductCategory> productCategories);
        Task DeleteAsync(int productId, int categoryId);
        Task DeleteByProductIdAsync(int productId);
        Task DeleteByCategoryIdAsync(int categoryId);
        // Task UpdateProductCategoriesAsync(int productId, IEnumerable<int> categoryIds);
    }
}
