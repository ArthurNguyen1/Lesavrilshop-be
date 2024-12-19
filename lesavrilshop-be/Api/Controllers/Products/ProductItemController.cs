// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using lesavrilshop_be.Core.DTOs.Products;
// using lesavrilshop_be.Core.Entities.Products;
// using lesavrilshop_be.Core.Interfaces.Repositories.Products;
// using Microsoft.AspNetCore.Mvc;

// namespace lesavrilshop_be.Api.Controllers.Products
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ProductItemController : ControllerBase
//     {
//         private readonly IProductItemRepository _productItemRepository;
//         private readonly IColorRepository _colorRepository;
//         private readonly ISizeRepository _sizeRepository;
//         private readonly ILogger<ProductItemController> _logger;

//         public ProductItemController(
//             IProductItemRepository productItemRepository,
//             IColorRepository colorRepository,
//             ISizeRepository sizeRepository,
//             ILogger<ProductItemController> logger)
//         {
//             _productItemRepository = productItemRepository;
//             _colorRepository = colorRepository;
//             _sizeRepository = sizeRepository;
//             _logger = logger;
//         }

//         [HttpGet("product/{productId}")]
//         public async Task<ActionResult<IEnumerable<ProductItemResponseDto>>> GetByProduct(int productId)
//         {
//             try
//             {
//                 var items = await _productItemRepository.GetByProductIdAsync(productId);
//                 return Ok(items.Select(MapToDto));
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error retrieving product items");
//                 return StatusCode(500, "Internal server error");
//             }
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<ProductItemResponseDto>> GetById(int id)
//         {
//             try
//             {
//                 var item = await _productItemRepository.GetByIdAsync(id);
//                 if (item == null)
//                     return NotFound();

//                 return Ok(MapToDto(item));
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error retrieving product item");
//                 return StatusCode(500, "Internal server error");
//             }
//         }

//         [HttpPost]
//         public async Task<ActionResult<ProductItemResponseDto>> Create(CreateProductItemDto createDto)
//         {
//             try
//             {
//                 var productItem = new ProductItem
//                 {
//                     ProductId = createDto.ProductId,
//                     SKU = createDto.SKU,
//                     OriginalPrice = createDto.OriginalPrice,
//                     SalePrice = createDto.SalePrice,
//                     QuantityInStock = createDto.QuantityInStock,
//                     CreatedAt = DateTime.UtcNow,
//                     UpdatedAt = DateTime.UtcNow
//                 };

//                 // Handle color
//                 if (!string.IsNullOrEmpty(createDto.ColorName))
//                 {
//                     var color = await _colorRepository.GetOrCreateByNameAsync(createDto.ColorName);
//                     productItem.ColorId = color.Id;
//                 }

//                 // Handle size
//                 if (!string.IsNullOrEmpty(createDto.SizeName))
//                 {
//                     var size = await _sizeRepository.GetOrCreateByNameAsync(createDto.SizeName);
//                     productItem.SizeId = size.Id;
//                 }

//                 var created = await _productItemRepository.CreateAsync(productItem);
//                 return CreatedAtAction(
//                     nameof(GetById),
//                     new { id = created.Id },
//                     MapToDto(created));
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error creating product item");
//                 return StatusCode(500, "Internal server error");
//             }
//         }

//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             try
//             {
//                 await _productItemRepository.DeleteAsync(id);
//                 return NoContent();
//             }
//             catch (KeyNotFoundException)
//             {
//                 return NotFound();
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error deleting product item");
//                 return StatusCode(500, "Internal server error");
//             }
//         }

//         private static ProductItemResponseDto MapToDto(ProductItem item)
//         {
//             return new ProductItemResponseDto
//             {
//                 Id = item.Id,
//                 SKU = item.SKU,
//                 OriginalPrice = item.OriginalPrice,
//                 SalePrice = item.SalePrice,
//                 QuantityInStock = item.QuantityInStock,
//                 Color = item.Color != null ? new ColorDto
//                 {
//                     Id = item.Color.Id,
//                     ColorName = item.Color.ColorName
//                 } : null,
//                 Size = item.Size != null ? new SizeDto
//                 {
//                     Id = item.Size.Id,
//                     SizeName = item.Size.SizeName
//                 } : null,
//                 CreatedAt = item.CreatedAt,
//                 UpdatedAt = item.UpdatedAt
//             };
//         }
//     }
// }