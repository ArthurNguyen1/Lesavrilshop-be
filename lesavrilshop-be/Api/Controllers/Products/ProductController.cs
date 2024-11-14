using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace lesavrilshop_be.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
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
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
        {
            try
            {
                if (productDto.ParentCategoryId.HasValue)
                {
                    var parentExists = await _productRepository.ExistsAsync(productDto.ParentCategoryId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new 
                        { 
                            error = "Parent category does not exist",
                            code = "INVALID_PARENT_CATEGORY"
                        });
                    }
                }

                var createdProduct = await _productRepository.CreateAsync(productDto);
                
                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = createdProduct.Id },
                    createdProduct);
            }
            catch (Exception ex)
            {
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
        
        [HttpGet("filterBySize")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterBySize([FromQuery] string sizeName)
        {
            if (string.IsNullOrEmpty(sizeName))
                return BadRequest("Size name must be provided");

            try
            {
                var products = await _productRepository.FilterBySizeAsync(sizeName);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering products by size");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("filterByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterByCategory([FromQuery] int categoryId)
        {
            try
            {
                var products = await _productRepository.FilterByCategoryAsync(categoryId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering products by category");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("sorted-products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetSortedProducts([FromQuery] string sortBy, [FromQuery] bool isAscending = true)
        {
            try
            {
                var products = await _productRepository.GetSortedProductsAsync(sortBy, isAscending);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sorting products by {SortBy} with ascending: {IsAscending}", sortBy, isAscending);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}