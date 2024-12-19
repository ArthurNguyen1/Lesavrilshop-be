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

        public List<string>? Colors { get; set; }
        public List<string>? Sizes { get; set; }
        public List<int>? CategoryIds { get; set; }

        // New property for image uploads
        [Required(ErrorMessage = "At least one product image is required")]
        public List<IFormFile> Images { get; set; } = new();

        [Required]
        public int MainImageIndex { get; set; } = 0;
    }
}