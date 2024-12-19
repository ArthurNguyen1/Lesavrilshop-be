using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public string? DeliveryDescription { get; set; }
        public decimal RatingAverage { get; set; }
        public int RatingQuantity { get; set; }
        public bool IsActive { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QuantityInStock { get; set; }
        public List<string> Colors { get; set; } = new();
        public List<string> Sizes { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<ProductImageDto> Images { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}