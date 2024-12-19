using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Core.Interfaces.Services;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Products
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly ILogger<ProductImageRepository> _logger;

        public ProductImageRepository(
            ApplicationDbContext context,
            IImageService imageService,
            ILogger<ProductImageRepository> logger)
        {
            _context = context;
            _imageService = imageService;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .OrderByDescending(pi => pi.IsMain)
                .ThenBy(pi => pi.Id)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetByIdAsync(int id)
        {
            return await _context.ProductImages
                .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<ProductImage> CreateAsync(ProductImage productImage)
        {
            var productExists = await _context.Products
        .AsNoTracking()
        .AnyAsync(p => p.Id == productImage.ProductId);

            if (!productExists)
            {
                _logger.LogError("Attempted to create image for non-existent product ID: {ProductId}", productImage.ProductId);
                throw new InvalidOperationException($"Product with ID {productImage.ProductId} does not exist");
            }

            // If this is set to be the main image, unset any existing main images
            if (productImage.IsMain)
            {
                var existingMainImages = await _context.ProductImages
                    .Where(pi => pi.ProductId == productImage.ProductId && pi.IsMain)
                    .ToListAsync();

                foreach (var mainImage in existingMainImages)
                {
                    mainImage.IsMain = false;
                    mainImage.UpdatedAt = DateTime.UtcNow;
                }
            }

            productImage.CreatedAt = DateTime.UtcNow;
            productImage.UpdatedAt = DateTime.UtcNow;

            // Add the product image to the context
            _context.ProductImages.Add(productImage);

            // Save changes
            await _context.SaveChangesAsync();

            return productImage;

        }

        public async Task UpdateAsync(ProductImage productImage)
        {
            var existingImage = await _context.ProductImages
                .FirstOrDefaultAsync(pi => pi.Id == productImage.Id);

            if (existingImage == null)
                throw new KeyNotFoundException($"ProductImage with ID {productImage.Id} not found");

            if (productImage.IsMain && !existingImage.IsMain)
                await UnsetMainImagesAsync(existingImage.ProductId);

            existingImage.IsMain = productImage.IsMain;
            existingImage.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productImage = await _context.ProductImages
                .FirstOrDefaultAsync(pi => pi.Id == id);

            if (productImage == null)
                throw new KeyNotFoundException($"ProductImage with ID {id} not found");

            // Delete from Cloudinary
            var publicId = GetPublicIdFromUrl(productImage.ImageUrl);
            if (!string.IsNullOrEmpty(publicId))
            {
                try
                {
                    await _imageService.DeleteImageAsync(publicId);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete image from Cloudinary: {ImageUrl}", productImage.ImageUrl);
                }
            }

            if (productImage.IsMain)
            {
                var nextImage = await _context.ProductImages
                    .Where(pi => pi.ProductId == productImage.ProductId && pi.Id != id)
                    .FirstOrDefaultAsync();

                if (nextImage != null)
                {
                    nextImage.IsMain = true;
                    nextImage.UpdatedAt = DateTime.UtcNow;
                }
            }

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SetMainImageAsync(int productId, int imageId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Unset current main image(s)
                var mainImages = await _context.ProductImages
                    .Where(pi => pi.ProductId == productId && pi.IsMain)
                    .ToListAsync();

                foreach (var image in mainImages)
                {
                    image.IsMain = false;
                    image.UpdatedAt = DateTime.UtcNow;
                }

                // Set new main image
                var newMainImage = await _context.ProductImages
                    .FirstOrDefaultAsync(pi => pi.Id == imageId && pi.ProductId == productId);

                if (newMainImage == null)
                    return false;

                newMainImage.IsMain = true;
                newMainImage.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> HasMainImageAsync(int productId)
        {
            return await _context.ProductImages
                .AnyAsync(pi => pi.ProductId == productId && pi.IsMain);
        }

        public async Task<string?> GetMainImageUrlAsync(int productId)
        {
            var mainImage = await _context.ProductImages
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IsMain);

            return mainImage?.ImageUrl;
        }

        private async Task UnsetMainImagesAsync(int productId)
        {
            var mainImages = await _context.ProductImages
                .Where(pi => pi.ProductId == productId && pi.IsMain)
                .ToListAsync();

            foreach (var image in mainImages)
            {
                image.IsMain = false;
                image.UpdatedAt = DateTime.UtcNow;
            }
        }

        private static string? GetPublicIdFromUrl(string imageUrl)
        {
            try
            {
                var uri = new Uri(imageUrl);
                var pathSegments = uri.AbsolutePath.Split('/');
                return Path.GetFileNameWithoutExtension(pathSegments[pathSegments.Length - 1]);
            }
            catch
            {
                return null;
            }
        }

        public Task<IEnumerable<ProductImage>> GetByProductItemIdAsync(int productItemId)
        {
            throw new NotImplementedException();
        }


    }
}