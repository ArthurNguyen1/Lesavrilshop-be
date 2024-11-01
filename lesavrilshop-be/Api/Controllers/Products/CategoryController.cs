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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryRepository categoryRepository,
            ILogger<CategoryController> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving category {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategoryDto categoryDto)
        {
            try
            {
                if (categoryDto.ParentCategoryId.HasValue)
                {
                    var parentExists = await _categoryRepository.ExistsAsync(categoryDto.ParentCategoryId.Value);
                    if (!parentExists)
                    {
                        return BadRequest(new 
                        { 
                            error = "Parent category does not exist",
                            code = "INVALID_PARENT_CATEGORY"
                        });
                    }
                }

                var createdCategory = await _categoryRepository.CreateAsync(categoryDto);
                
                return CreatedAtAction(
                    nameof(GetCategory),
                    new { id = createdCategory.Id },
                    createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            try
            {
                await _categoryRepository.UpdateAsync(category);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting category {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/subcategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubcategories(int id)
        {
            try
            {
                var exists = await _categoryRepository.ExistsAsync(id);
                if (!exists)
                    return NotFound();

                var subcategories = await _categoryRepository.GetSubcategoriesAsync(id);
                return Ok(subcategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subcategories for category {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}