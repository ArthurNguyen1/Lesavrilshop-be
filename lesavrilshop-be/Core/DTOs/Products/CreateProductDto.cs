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
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string DeliveryDescription { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}