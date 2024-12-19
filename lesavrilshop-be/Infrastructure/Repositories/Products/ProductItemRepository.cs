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
    public class ProductItemRepository : IProductItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductItemRepository> _logger;

        public ProductItemRepository(ApplicationDbContext context, ILogger<ProductItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductItem>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductItems
                .Include(pi => pi.Color)
                .Include(pi => pi.Size)
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductItem?> GetByIdAsync(int id)
        {
            return await _context.ProductItems
                .Include(pi => pi.Color)
                .Include(pi => pi.Size)
                .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<ProductItem> CreateAsync(ProductItem productItem)
        {
            await _context.ProductItems.AddAsync(productItem);
            await _context.SaveChangesAsync();

            // Reload the product item with all relationships
            return await _context.ProductItems
                .Include(pi => pi.Color)
                .Include(pi => pi.Size)
                .FirstOrDefaultAsync(pi => pi.Id == productItem.Id)
                ?? throw new Exception($"Failed to retrieve created product item with ID: {productItem.Id}");
        }

        public async Task UpdateAsync(ProductItem productItem)
        {
            var existing = await _context.ProductItems.FindAsync(productItem.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Product item with ID {productItem.Id} not found");

            _context.Entry(existing).CurrentValues.SetValues(productItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem == null)
                throw new KeyNotFoundException($"Product item with ID {id} not found");

            _context.ProductItems.Remove(productItem);
            await _context.SaveChangesAsync();
        }
    }
}