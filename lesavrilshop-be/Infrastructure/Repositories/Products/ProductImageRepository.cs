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
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            return await _context.ProductImages.ToListAsync();
        }

        public async Task<ProductImage> GetByIdAsync(int id)
        {
            return await _context.ProductImages.FindAsync(id);
        }

        public async Task<ProductImage> CreateAsync(CreateProductImageDto productImageDto)
        {
            var productImage = new ProductImage(
                productImageDto.ImageUrl,
                productImageDto.IsMain,
                productImageDto.ProductId
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            
            return productImage;
        }

        public async Task UpdateAsync(ProductImage productImage)
        {
            var existingProductImage = await _context.ProductImages.FindAsync(productImage.Id);
            if (existingProductImage == null)
                throw new KeyNotFoundException($"ProductImage with ID {productImage.Id} not found");

            existingProductImage.ProductId =  productImage.ProductId;
            existingProductImage.ImageUrl = productImage.ImageUrl;
            existingProductImage.IsMain = productImage.IsMain;
            existingProductImage.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
                throw new KeyNotFoundException($"ProductImage with ID {id} not found");

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ProductImages.AnyAsync(pi => pi.Id == id);
        }
    }
}