using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Products
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetByProductItemIdAsync(int productItemId);
        Task<ProductImage?> GetByIdAsync(int id);
        Task<ProductImage> CreateAsync(ProductImage productImage);
        Task UpdateAsync(ProductImage productImage);
        Task DeleteAsync(int id);
        Task<bool> SetMainImageAsync(int productItemId, int imageId);
        Task<bool> HasMainImageAsync(int productItemId);
        Task<string?> GetMainImageUrlAsync(int productItemId);
    }
}