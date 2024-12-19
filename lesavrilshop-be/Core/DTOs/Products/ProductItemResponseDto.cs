using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class ProductItemResponseDto
    {
        public int Id { get; set; }
        public string SKU { get; set; } = default!;
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}