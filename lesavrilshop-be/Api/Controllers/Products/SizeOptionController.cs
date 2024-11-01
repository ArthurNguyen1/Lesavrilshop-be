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
    public class SizeOptionController : ControllerBase
    {
        private readonly ISizeOptionRepository _sizeOptionRepository;
        private readonly ILogger<SizeOptionController> _logger;

        public SizeOptionController(
            ISizeOptionRepository sizeOptionRepository,
            ILogger<SizeOptionController> logger)
        {
            _sizeOptionRepository = sizeOptionRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeOption>>> GetSizeOptions()
        {
            try
            {
                var sizeOptions = await _sizeOptionRepository.GetAllAsync();
                return Ok(sizeOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sizeOptions");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SizeOption>> GetSizeOption(int id)
        {
            try
            {
                var sizeOption = await _sizeOptionRepository.GetByIdAsync(id);
                if (sizeOption == null)
                    return NotFound();

                return Ok(sizeOption);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sizeOption {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SizeOption>> CreateSizeOption(CreateSizeOptionDto sizeOptionDto)
        {
            try
            {
                var createdSizeOption = await _sizeOptionRepository.CreateAsync(sizeOptionDto);
                
                return CreatedAtAction(
                    nameof(GetSizeOption),
                    new { id = createdSizeOption.Id },
                    createdSizeOption);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sizeOption");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSizeOption(int id, SizeOption sizeOption)
        {
            if (id != sizeOption.Id)
                return BadRequest();

            try
            {
                await _sizeOptionRepository.UpdateAsync(sizeOption);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sizeOption {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizeOption(int id)
        {
            try
            {
                await _sizeOptionRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting sizeOption {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}