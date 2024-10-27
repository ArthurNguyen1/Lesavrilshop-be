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
    public class ProductItemRepository : IProductItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductItem>> GetAllAsync()
        {
            return await _context.ProductItems.ToListAsync();
                
        }

        public async Task<ProductItem> GetByIdAsync(int id)
        {
            return await _context.ProductItems.FindAsync(id);
        }

        public async Task<ProductItem> CreateAsync(CreateProductItemDto productItemDto)
        {
            var productItem = new ProductItem(
                productItemDto.ProductId,
                productItemDto.ColorId,
                productItemDto.SizeId,
                productItemDto.OriginalPrice,
                productItemDto.SalePrice,
                productItemDto.QuantityInStock
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.ProductItems.Add(productItem);
            await _context.SaveChangesAsync();
            
            return productItem;
        }

        public async Task UpdateAsync(ProductItem productItem)
        {
            var existingProductItem = await _context.ProductItems.FindAsync(productItem.Id);
            if (existingProductItem == null)
                throw new KeyNotFoundException($"ProductItem with ID {productItem.Id} not found");

            existingProductItem.ProductId = productItem.ProductId;
            existingProductItem.ColorId = productItem.ColorId;
            existingProductItem.SizeId = productItem.SizeId;
            existingProductItem.OriginalPrice = productItem.OriginalPrice;
            existingProductItem.SalePrice = productItem.SalePrice;
            existingProductItem.QuantityInStock = productItem.QuantityInStock;
            existingProductItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem == null)
                throw new KeyNotFoundException($"ProductItem with ID {id} not found");

            _context.ProductItems.Remove(productItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ProductItems.AnyAsync(pi => pi.Id == id);
        }
    }
}