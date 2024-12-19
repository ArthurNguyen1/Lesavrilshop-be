using System.Text.Json;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace lesavrilshop_be.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService,
            IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _productRepository = productRepository;
            _logger = logger;
        }


        /// <summary>
        /// Get products by category ID (includes products from subcategories)
        /// </summary>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<PaginatedResult<ProductResponseDto>>> GetProductsByCategory(
            int categoryId,
            [FromQuery] ProductFilterParams filterParams)
        {
            try
            {
                var result = await _productRepository.GetByCategoryAsync(categoryId, filterParams);
                return Ok(MapToPaginatedDto(result));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for category {CategoryId}", categoryId);
                return StatusCode(500, "An error occurred while retrieving products");
            }
        }

        /// <summary>
        /// Get all products
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResponseDto>>> GetProducts(
            [FromQuery] ProductFilterParams filterParams)
        {
            try
            {
                var result = await _productRepository.GetAllAsync(filterParams);
                return Ok(MapToPaginatedDto(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                return StatusCode(500, "An error occurred while retrieving products");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<PaginatedResult<ProductResponseDto>>> SearchProducts(
            [FromQuery] ProductFilterParams filterParams)
        {
            try
            {
                var result = await _productRepository.SearchAsync(filterParams);
                return Ok(MapToPaginatedDto(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products");
                return StatusCode(500, "An error occurred while searching products");
            }
        }

        /// <summary>
        /// Get a specific product by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(
            int id,
            [FromQuery] bool includeInactive = false)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id, includeInactive);
                if (product == null)
                    return NotFound();

                return Ok(MapToProductDto(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product {ProductId}", id);
                return StatusCode(500, "An error occurred while retrieving the product");
            }
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 10485760)] // 10MB
        [RequestSizeLimit(10485760)] // 10MB
        public async Task<ActionResult<ProductResponseDto>> CreateProduct(
            [FromForm] CreateProductDto productDto)
        {
            try
            {
                // Validate file types
                foreach (var image in productDto.Images)
                {
                    if (!IsValidImageFile(image))
                    {
                        return BadRequest($"Invalid file type for {image.FileName}. Only jpg, jpeg, png, and gif are allowed.");
                    }
                }

                var product = await _productService.CreateProductWithImagesAsync(productDto);

                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = product.Id },
                    MapToProductDto(product));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, "An error occurred while creating the product");
            }
        }

        // Other existing endpoints remain the same...

        private static bool IsValidImageFile(IFormFile file)
        {
            // Check file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                return false;

            // Check file signature (magic numbers)
            var signatures = new Dictionary<string, List<byte[]>>
            {
                { ".jpg", new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF } } },
                { ".jpeg", new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF } } },
                { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47 } } },
                { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } }
            };

            if (signatures.TryGetValue(extension, out var validSignatures))
            {
                using var reader = new BinaryReader(file.OpenReadStream());
                var headerBytes = reader.ReadBytes(4);

                return validSignatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }

            return false;
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] UpdateProductDto productDto)
        {
            try
            {
                if (!await _productRepository.ExistsAsync(id))
                    return NotFound();

                await _productRepository.UpdateAsync(id, productDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", id);
                return StatusCode(500, "An error occurred while updating the product");
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {ProductId}", id);
                return StatusCode(500, "An error occurred while deleting the product");
            }
        }

        /// <summary>
        /// Maps a Product entity to a ProductResponseDto
        /// </summary>
        private static ProductResponseDto MapToProductDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                ProductDescription = product.ProductDescription,
                DeliveryDescription = product.DeliveryDescription,
                RatingAverage = product.RatingAverage,
                RatingQuantity = product.RatingQuantity,
                IsActive = product.IsActive,
                OriginalPrice = product.OriginalPrice,
                SalePrice = product.SalePrice,
                QuantityInStock = product.QuantityInStock,
                Colors = product.Colors.ToList(),
                Sizes = product.Sizes.ToList(),
                Categories = product.ProductCategories?
            .Where(pc => pc.Category != null)
            .Select(pc => new CategoryDto
            {
                Id = pc.Category!.Id,
                Name = pc.Category.Name,
                ParentCategoryId = pc.Category.ParentCategoryId
            })
            .ToList() ?? new List<CategoryDto>(),
                Images = product.Images?
            .Select(i => new ProductImageDto
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                IsMain = i.IsMain
            })
            .ToList() ?? new List<ProductImageDto>(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private static PaginatedResult<ProductResponseDto> MapToPaginatedDto(
            PaginatedResult<Product> result)
        {
            return new PaginatedResult<ProductResponseDto>
            {
                Items = result.Items.Select(MapToProductDto),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }
    }
}