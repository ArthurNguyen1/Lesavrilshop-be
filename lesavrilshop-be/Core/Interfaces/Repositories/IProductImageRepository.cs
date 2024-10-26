using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetAllAsync();
        Task<ProductImage> GetByIdAsync(int id);
        Task<ProductImage> CreateAsync(CreateProductImageDto productImageDto);
        Task UpdateAsync(ProductImage productImage);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}