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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductItems)
                .Include(p => p.ProductCategories)
                .ToListAsync();
                
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductItems)
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(CreateProductDto productDto)
        {
            var product = new Product(
                productDto.Name,
                productDto.ProductDescription,
                productDto.DeliveryDescription,
                productDto.ParentCategoryId
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {product.Id} not found");

            existingProduct.Name = product.Name;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.DeliveryDescription = product.DeliveryDescription;
            existingProduct.ParentCategoryId = product.ParentCategoryId;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Product>> FilterBySizeAsync(string sizeName)
        {
            return await _context.Products
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.Size)
                .Where(p => p.ProductItems.Any(pi => pi.Size.SizeName == sizeName))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync();
                // .Include(p => p.ProductItems)  // Includes related ProductItems
                // .Include(p => p.ProductCategories)
                //     .ThenInclude(pc => pc.Category) // Includes related Categories through ProductCategory
                // .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
                // .ToListAsync();
        }

    }
}