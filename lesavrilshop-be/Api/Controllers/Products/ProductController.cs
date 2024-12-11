using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using lesavrilshop_be.Infrastructure.Data;


namespace lesavrilshop_be.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IImageService _imageService;
        private readonly ApplicationDbContext _context;

        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductRepository productRepository,
            IProductItemRepository productItemRepository,
            IProductImageRepository productImageRepository,
            IImageService imageService,
            ApplicationDbContext context,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _productItemRepository = productItemRepository;
            _productImageRepository = productImageRepository;
            _imageService =imageService;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] CreateProductDto createProductDTO,  IFormFile? mainImage)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (createProductDTO.ParentCategoryId.HasValue)
                {
                    var parentExists = await _productRepository.ExistsAsync(createProductDTO.ParentCategoryId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new 
                        { 
                            error = "Parent category does not exist",
                            code = "INVALID_PARENT_CATEGORY"
                        });
                    }
                }

                // Handle image upload if provided
                string? uploadedImageUrl = null;
                if (mainImage != null && mainImage.Length > 0)
                {
                    var uploadResult = await _imageService.AddImageAsync(mainImage);
                    if (uploadResult == null)
                        return BadRequest("Image upload failed");
                    uploadedImageUrl = uploadResult.SecureUrl.AbsoluteUri;
                }

                // Create the product entity
                var createdProduct = await _productRepository.CreateAsync(createProductDTO);

                // Create product item entity
                var createProductItemDto = new CreateProductItemDto
                {
                    OriginalPrice = createProductDTO.OriginalPrice,
                    SalePrice = createProductDTO.SalePrice,
                    QuantityInStock = createProductDTO.QuantityInStock,
                    ProductId = createdProduct.Id,
                    ColorId = createProductDTO.ColorId,
                    SizeId = createProductDTO.SizeId,
                };
                var createdProductItem = await _productItemRepository.CreateAsync(createProductItemDto);

                var createProductImageDto = new CreateProductImageDto
                {
                    ProductItemId = createdProductItem.Id,
                    ImageUrl = uploadedImageUrl,
                    IsMain = true
                };
                await _productImageRepository.CreateAsync(createProductImageDto);

                await transaction.CommitAsync();

                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = createdProduct.Id },
                    createdProduct);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating product");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            try
            {
                await _productRepository.UpdateAsync(product);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

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
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterAndSortProducts(
            [FromQuery] int? sizeId,
            [FromQuery] int? colorId,
            [FromQuery] int? categoryId,
            [FromQuery] string? sortOrder = "name")
        {
            try
            {
                var products = await _productRepository.GetFilterAndSortedProductsAsync(sizeId, colorId, categoryId, sortOrder);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filter products");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("search-products")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string? keyword = null)
        {
            try
            {
                var products = await _productRepository.SearchProductsAsync(keyword);

                if (!products.Any())
                {
                    return NotFound("No products found.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products with keyword: {Keyword}", keyword);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}