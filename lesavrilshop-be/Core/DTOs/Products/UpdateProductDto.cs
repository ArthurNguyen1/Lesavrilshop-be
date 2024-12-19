using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [Required]
        public string ProductDescription { get; set; } = default!;

        public string? DeliveryDescription { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal OriginalPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SalePrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        [Required]
        public List<string> Colors { get; set; } = [];

        [Required]
        public List<string> Sizes { get; set; } = [];

        [Required]
        public List<int> CategoryIds { get; set; } = [];

        public bool IsActive { get; set; } = true;

    }
}