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

        public List<string> Colors { get; set; }
        public List<string> Sizes { get; set; }
    }
}