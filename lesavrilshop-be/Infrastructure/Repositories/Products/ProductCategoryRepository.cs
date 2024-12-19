using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Products
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductCategoryRepository> _logger;

        public ProductCategoryRepository(
            ApplicationDbContext context,
            ILogger<ProductCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductCategory>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductCategories
                .Include(pc => pc.Category)
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.ProductCategories
                .Include(pc => pc.Product)
                .Where(pc => pc.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int productId, int categoryId)
        {
            return await _context.ProductCategories
                .AnyAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory productCategory)
        {
            // Validate Product exists
            var productExists = await _context.Products
                .AnyAsync(p => p.Id == productCategory.ProductId);
            if (!productExists)
            {
                throw new InvalidOperationException($"Product with ID {productCategory.ProductId} does not exist");
            }

            // Validate Category exists
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == productCategory.CategoryId);
            if (!categoryExists)
            {
                throw new InvalidOperationException($"Category with ID {productCategory.CategoryId} does not exist");
            }

            // Check if relationship already exists
            var exists = await ExistsAsync(productCategory.ProductId, productCategory.CategoryId);
            if (exists)
            {
                throw new InvalidOperationException("This product-category relationship already exists");
            }

            // Set timestamps
            productCategory.CreatedAt = DateTime.UtcNow;
            productCategory.UpdatedAt = DateTime.UtcNow;

            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return productCategory;
        }

        public async Task CreateRangeAsync(IEnumerable<ProductCategory> productCategories)
        {
            // Validate all products and categories exist
            var productIds = productCategories.Select(pc => pc.ProductId).Distinct();
            var categoryIds = productCategories.Select(pc => pc.CategoryId).Distinct();

            var existingProducts = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync();

            var existingCategories = await _context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToListAsync();

            var now = DateTime.UtcNow;
            foreach (var pc in productCategories)
            {
                if (!existingProducts.Contains(pc.ProductId))
                {
                    throw new InvalidOperationException($"Product with ID {pc.ProductId} does not exist");
                }

                if (!existingCategories.Contains(pc.CategoryId))
                {
                    throw new InvalidOperationException($"Category with ID {pc.CategoryId} does not exist");
                }

                pc.CreatedAt = now;
                pc.UpdatedAt = now;
            }

            _context.ProductCategories.AddRange(productCategories);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, int categoryId)
        {
            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

            if (productCategory == null)
            {
                throw new KeyNotFoundException($"Product-Category relationship not found for Product ID {productId} and Category ID {categoryId}");
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByProductIdAsync(int productId)
        {
            var productCategories = await _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();

            if (productCategories.Any())
            {
                _context.ProductCategories.RemoveRange(productCategories);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByCategoryIdAsync(int categoryId)
        {
            var productCategories = await _context.ProductCategories
                .Where(pc => pc.CategoryId == categoryId)
                .ToListAsync();

            if (productCategories.Any())
            {
                _context.ProductCategories.RemoveRange(productCategories);
                await _context.SaveChangesAsync();
            }
        }


    }
}