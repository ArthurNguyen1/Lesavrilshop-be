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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(
            IProductCategoryRepository productCategoryRepository,
            ILogger<ProductCategoryController> logger)
        {
            _productCategoryRepository = productCategoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            try
            {
                var productCategories = await _productCategoryRepository.GetAllAsync();
                return Ok(productCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving productCategories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            try
            {
                var productCategory = await _productCategoryRepository.GetByIdAsync(id);
                if (productCategory == null)
                    return NotFound();

                return Ok(productCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving productCategory {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductCategory>> CreateProductCategory(CreateProductCategoryDto productCategoryDto)
        {
            try
            {
                var createdProductCategory = await _productCategoryRepository.CreateAsync(productCategoryDto);
                
                return CreatedAtAction(
                    nameof(GetProductCategory),
                    new { id = createdProductCategory.Id },
                    createdProductCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating productCategory");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
                return BadRequest();

            try
            {
                await _productCategoryRepository.UpdateAsync(productCategory);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating productCategory {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            try
            {
                await _productCategoryRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting productCategory {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}