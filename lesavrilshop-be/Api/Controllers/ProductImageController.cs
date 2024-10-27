using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace lesavrilshop_be.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductItemRepository _productItemRepository;

        private readonly ILogger<ProductImageController> _logger;

        public ProductImageController(
            IProductImageRepository productImageRepository,
            IProductItemRepository productItemRepository,
            ILogger<ProductImageController> logger)
        {
            _productImageRepository = productImageRepository;
            _productItemRepository = productItemRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
        {
            try
            {
                var productImages = await _productImageRepository.GetAllAsync();
                return Ok(productImages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving productImages");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(int id)
        {
            try
            {
                var productImage = await _productImageRepository.GetByIdAsync(id);
                if (productImage == null)
                    return NotFound();

                return Ok(productImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving productImage {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductImage>> CreateProductImage(CreateProductImageDto productImageDto)
        {
            try
            {
                if (productImageDto.ProductItemId.HasValue)
                {
                    var parentExists = await _productItemRepository.ExistsAsync(productImageDto.ProductItemId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new 
                        { 
                            error = "Parent productImage does not exist",
                            code = "INVALID_PARENT_PRODUCTIMAGE"
                        });
                    }
                }
                
                var createdProductImage = await _productImageRepository.CreateAsync(productImageDto);
                
                return CreatedAtAction(
                    nameof(GetProductImage),
                    new { id = createdProductImage.Id },
                    createdProductImage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating productImage");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.Id)
                return BadRequest();

            try
            {
                await _productImageRepository.UpdateAsync(productImage);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating productImage {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            try
            {
                await _productImageRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting productImage {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}