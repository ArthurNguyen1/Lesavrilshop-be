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
    public class ShopOrderController : ControllerBase
    {
        private readonly IShopOrderRepository _ShopOrderRepository;
        private readonly IOrderItemRepository _OrderItemRepository;
        private readonly ILogger<ShopOrderController> _logger;
        private readonly IConfiguration _config;

        public ShopOrderController(
            IShopOrderRepository ShopOrderRepository,
            IOrderItemRepository OrderItemRepository,
            ILogger<ShopOrderController> logger,
            IConfiguration config)
        {
            _ShopOrderRepository = ShopOrderRepository;
            _OrderItemRepository = OrderItemRepository;
            _logger = logger;
            _config = config;;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopOrder>>> GetShopOrders()
        {
            try
            {
                var ShopOrders = await _ShopOrderRepository.GetAllAsync();
                return Ok(ShopOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ShopOrders");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShopOrder>> GetShopOrder(int id)
        {
            try
            {
                var ShopOrder = await _ShopOrderRepository.GetByIdAsync(id);
                if (ShopOrder == null)
                    return NotFound();

                return Ok(ShopOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ShopOrder {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShopOrder>> CreateShopOrder(CreateShopOrderDto ShopOrderDto, [FromQuery] List<int> productItemId)
        {
            try
            {
                var createdShopOrder = await _ShopOrderRepository.CreateAsync(ShopOrderDto);
                
                // Create OrderItems associated with this ShopOrder
                foreach (var id in productItemId)
                {
                    var orderItemDto = new CreateOrderItemDto
                    {
                        ProductItemId = id,
                        OrderId = createdShopOrder.Id,
                    };
                    await _OrderItemRepository.CreateAsync(orderItemDto);
                }

                return CreatedAtAction(
                    nameof(GetShopOrder),
                    new { id = createdShopOrder.Id },
                    createdShopOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ShopOrder");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopOrder(int id, ShopOrder ShopOrder)
        {
            if (id != ShopOrder.Id)
                return BadRequest();

            try
            {
                await _ShopOrderRepository.UpdateAsync(ShopOrder);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ShopOrder {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopOrder(int id)
        {
            try
            {
                await _ShopOrderRepository.DeleteAsync(id);
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
                _logger.LogError(ex, "Error deleting ShopOrder {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id}/pay/stripe")]
        public async Task<IActionResult> PayWithStripe(int id)
        {
            var order = await _ShopOrderRepository.GetByIdAsync(id);
            if (order == null) return NotFound("Order not found");

            if (order.OrderStatusId == 2) // assume that StatusId 2 is Paid
                return BadRequest("Order is already paid.");

            var clientSecret = await _ShopOrderRepository.CreateStripePaymentAsync(order);
            return Ok(new { clientSecret });
        }
    }
}