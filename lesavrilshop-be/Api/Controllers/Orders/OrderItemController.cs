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
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(
            IOrderItemRepository orderItemRepository,
            ILogger<OrderItemController> logger)
        {
            _orderItemRepository = orderItemRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            try
            {
                var orderItems = await _orderItemRepository.GetAllAsync();
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orderItems");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                if (orderItem == null)
                    return NotFound();

                return Ok(orderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving orderItem {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(CreateOrderItemDto orderItemDto)
        {
            try
            {
                var createdOrderItem = await _orderItemRepository.CreateAsync(orderItemDto);
                
                return CreatedAtAction(
                    nameof(GetOrderItem),
                    new { id = createdOrderItem.Id },
                    createdOrderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating orderItem");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
                return BadRequest();

            try
            {
                await _orderItemRepository.UpdateAsync(orderItem);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating orderItem {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                await _orderItemRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting orderItem {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}