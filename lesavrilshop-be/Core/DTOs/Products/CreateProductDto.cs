using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [Required]
        public string ProductDescription { get; set; } = default!;

        [Required]
        public string? DeliveryDescription { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public int? ColorId { get; set; }

        public int? SizeId { get; set; }
    }
}