using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Orders;
using lesavrilshop_be.Core.Entities.Orders;
using lesavrilshop_be.Core.Interfaces.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;

namespace lesavrilshop_be.Api.Controllers.Orders
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodRepository _ShippingMethodRepository;
        private readonly ILogger<ShippingMethodController> _logger;

        public ShippingMethodController(
            IShippingMethodRepository ShippingMethodRepository,
            ILogger<ShippingMethodController> logger)
        {
            _ShippingMethodRepository = ShippingMethodRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingMethod>>> GetShippingMethods()
        {
            try
            {
                var ShippingMethods = await _ShippingMethodRepository.GetAllAsync();
                return Ok(ShippingMethods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ShippingMethods");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingMethod>> GetShippingMethod(int id)
        {
            try
            {
                var ShippingMethod = await _ShippingMethodRepository.GetByIdAsync(id);
                if (ShippingMethod == null)
                    return NotFound();

                return Ok(ShippingMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ShippingMethod {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShippingMethod>> CreateShippingMethod(CreateShippingMethodDto ShippingMethodDto)
        {
            try
            {
                var createdShippingMethod = await _ShippingMethodRepository.CreateAsync(ShippingMethodDto);
                
                return CreatedAtAction(
                    nameof(GetShippingMethod),
                    new { id = createdShippingMethod.Id },
                    createdShippingMethod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ShippingMethod");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShippingMethod(int id, ShippingMethod ShippingMethod)
        {
            if (id != ShippingMethod.Id)
                return BadRequest();

            try
            {
                await _ShippingMethodRepository.UpdateAsync(ShippingMethod);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ShippingMethod {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingMethod(int id)
        {
            try
            {
                await _ShippingMethodRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting ShippingMethod {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}