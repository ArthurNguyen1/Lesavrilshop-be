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
                .Include(p => p.ProductCategories)
                .ToListAsync();
                
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(CreateProductDto productDto)
        {
            var product = new Product(
                productDto.Name,
                productDto.ProductDescription,
                productDto.DeliveryDescription
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            product.Colors = productDto.Colors;
            product.Sizes = productDto.Sizes;
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
            existingProduct.IsActive = product.IsActive;
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

        public async Task<IEnumerable<Product>> GetFilterAndSortedProductsAsync(int? sizeId, int? colorId, int? categoryId, string? sortOrder = "name", string? keyword = null)
        {
            var query = _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.ProductCategories)
                    .AsQueryable();
                    
            // Filter by category
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId.Value));
            }

            // Filter by size
            if (sizeId.HasValue)
            {
                query = query.Where(p => p.Sizes.Contains(sizeId.Value.ToString()));
                //query = query.Where(p => p.Sizes.Any(s => s.Id == sizeId.Value));
            }

            // Filter by color
            if (colorId.HasValue)
            {
                query = query.Where(p => p.Colors.Contains(colorId.Value.ToString()));
                //query = query.Where(p => p.Colors.Any(c => c.Id == colorId.Value));
            }

            // Apply search keyword
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(keyword));
            }

            // Sorting
            query = sortOrder?.ToLower() switch
            {
                "rating_desc" => query.OrderByDescending(p => p.RatingAverage),
                "rating_asc" => query.OrderBy(p => p.RatingAverage),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name) // Default to sorting by name
            };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string? keyword = null)
        {
            var query = _context.Products
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(keyword));
            }

            return await query.ToListAsync();
        }
    }
}