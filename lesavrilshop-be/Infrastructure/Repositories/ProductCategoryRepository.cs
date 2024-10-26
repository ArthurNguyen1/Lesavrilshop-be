using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.ProductCategories.ToListAsync();
                
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task<ProductCategory> CreateAsync(CreateProductCategoryDto productCategoryDto)
        {
            var productCategory = new ProductCategory(
                productCategoryDto.CategoryId,
                productCategoryDto.ProductId
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();
            
            return productCategory;
        }

        public async Task UpdateAsync(ProductCategory productCategory)
        {
            var existingProductCategory = await _context.ProductCategories.FindAsync(productCategory.Id);
            if (existingProductCategory == null)
                throw new KeyNotFoundException($"ProductCategory with ID {productCategory.Id} not found");

            existingProductCategory.CategoryId = productCategory.CategoryId;
            existingProductCategory.ProductId = productCategory.ProductId;
            existingProductCategory.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
                throw new KeyNotFoundException($"ProductCategory with ID {id} not found");

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ProductCategories.AnyAsync(p => p.Id == id);
        }
    }
}