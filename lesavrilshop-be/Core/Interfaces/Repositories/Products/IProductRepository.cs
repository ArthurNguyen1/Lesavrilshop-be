using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Products
{
    public interface IProductRepository
    {
        Task<PaginatedResult<Product>> GetAllAsync(ProductFilterParams filterParams);
        Task<PaginatedResult<Product>> GetByCategoryAsync(int categoryId, ProductFilterParams filterParams);
        Task<PaginatedResult<Product>> SearchAsync(ProductFilterParams filterParams);
        Task<Product?> GetByIdAsync(int id, bool includeInactive = false);
        Task<Product> CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(int id, UpdateProductDto productDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}