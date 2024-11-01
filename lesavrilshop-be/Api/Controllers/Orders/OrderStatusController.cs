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
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly ILogger<OrderStatusController> _logger;

        public OrderStatusController(
            IOrderStatusRepository orderStatusRepository,
            ILogger<OrderStatusController> logger)
        {
            _orderStatusRepository = orderStatusRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetOrderStatuses()
        {
            try
            {
                var orderStatuses = await _orderStatusRepository.GetAllAsync();
                return Ok(orderStatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orderStatuses");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus(int id)
        {
            try
            {
                var orderStatus = await _orderStatusRepository.GetByIdAsync(id);
                if (orderStatus == null)
                    return NotFound();

                return Ok(orderStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orderStatus {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderStatus>> CreateOrderStatus(CreateOrderStatusDto orderStatusDto)
        {
            try
            {
                var createdOrderStatus = await _orderStatusRepository.CreateAsync(orderStatusDto);
                
                return CreatedAtAction(
                    nameof(GetOrderStatus),
                    new { id = createdOrderStatus.Id },
                    createdOrderStatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating orderStatus");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
                return BadRequest();

            try
            {
                await _orderStatusRepository.UpdateAsync(orderStatus);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating orderStatus {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id)
        {
            try
            {
                await _orderStatusRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting orderStatus {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}