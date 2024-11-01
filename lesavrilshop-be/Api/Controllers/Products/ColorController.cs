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
    public class ColorController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;
        private readonly ILogger<ColorController> _logger;

        public ColorController(
            IColorRepository colorRepository,
            ILogger<ColorController> logger)
        {
            _colorRepository = colorRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            try
            {
                var colors = await _colorRepository.GetAllAsync();
                return Ok(colors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving colors");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(int id)
        {
            try
            {
                var color = await _colorRepository.GetByIdAsync(id);
                if (color == null)
                    return NotFound();

                return Ok(color);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving color {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Color>> CreateColor(CreateColorDto colorDto)
        {
            try
            {
                var createdColor = await _colorRepository.CreateAsync(colorDto);
                
                return CreatedAtAction(
                    nameof(GetColor),
                    new { id = createdColor.Id },
                    createdColor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating color");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateColor(int id, Color color)
        {
            if (id != color.Id)
                return BadRequest();

            try
            {
                await _colorRepository.UpdateAsync(color);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating color {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            try
            {
                await _colorRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting color {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}