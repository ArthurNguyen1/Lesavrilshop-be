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
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemRepository _productItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductItemController> _logger;

        public ProductItemController(
            IProductItemRepository productItemRepository,
            IProductRepository productRepository,
            ILogger<ProductItemController> logger)
        {
            _productItemRepository = productItemRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProductItems()
        {
            try
            {
                var productItems = await _productItemRepository.GetAllAsync();
                return Ok(productItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving productItems");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(int id)
        {
            try
            {
                var productItem = await _productItemRepository.GetByIdAsync(id);
                if (productItem == null)
                    return NotFound();

                return Ok(productItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving productItem {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductItem>> CreateProductItem(CreateProductItemDto productItemDto)
        {
            try
            {
                if (productItemDto.ProductId.HasValue)
                {
                    var parentExists = await _productRepository.ExistsAsync(productItemDto.ProductId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new 
                        { 
                            error = "Parent ProductItem does not exist",
                            code = "INVALID_PARENT_PRODUCTITEM"
                        });
                    }
                }

                var createdProductItem = await _productItemRepository.CreateAsync(productItemDto);
                
                return CreatedAtAction(
                    nameof(GetProductItem),
                    new { id = createdProductItem.Id },
                    createdProductItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating productItem");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductItem(int id, ProductItem productItem)
        {
            if (id != productItem.Id)
                return BadRequest();

            try
            {
                await _productItemRepository.UpdateAsync(productItem);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating productItem {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductItem(int id)
        {
            try
            {
                await _productItemRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting productItem {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}