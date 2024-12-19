using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class UpdateProductItemDto
    {
        public int Id { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string SKU { get; set; } = default!;
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}