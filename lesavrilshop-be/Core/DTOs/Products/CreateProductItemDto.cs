using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class CreateProductItemDto
    {

        public string? ColorName { get; set; }
        public string? SizeName { get; set; }

        [Required]
        public string SKU { get; set; } = default!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal OriginalPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SalePrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }
    }
}