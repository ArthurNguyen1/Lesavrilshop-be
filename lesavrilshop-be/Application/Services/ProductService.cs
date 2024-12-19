using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Core.Interfaces.Services;

namespace lesavrilshop_be.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IImageService _imageService;
        private readonly ILogger<ProductService> _logger;


        public ProductService(
            IProductRepository productRepository,
            IProductImageRepository productImageRepository,
            IImageService imageService,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
            _imageService = imageService;
            _logger = logger;
        }

        public async Task<Product> CreateProductWithImagesAsync(CreateProductDto productDto)
        {
            // Validate main image index
            if (productDto.MainImageIndex >= productDto.Images.Count)
            {
                throw new ArgumentException("Main image index is out of range");
            }

            // Start a transaction since we're making multiple database operations
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // 1. Create the product first
                var product = await _productRepository.CreateAsync(productDto);

                // 2. Upload and create product images
                var productImages = new List<ProductImage>();
                for (int i = 0; i < productDto.Images.Count; i++)
                {
                    var image = productDto.Images[i];

                    // Upload image to Cloudinary
                    var imageUrl = await _imageService.UploadImageAsync(image, "products");

                    // Create product image entity
                    var productImage = new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = imageUrl,
                        IsMain = i == productDto.MainImageIndex,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _productImageRepository.CreateAsync(productImage);
                    productImages.Add(productImage);
                }

                // 3. Add images to product entity
                product.Images = productImages;

                // 4. Commit transaction
                transaction.Complete();

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product with images");
                throw; // Re-throw to be handled by the controller
            }
        }
    }
}