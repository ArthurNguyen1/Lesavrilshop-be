using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class CreateProductImageDto
    {
        public int? ProductItemId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = default!;
        public bool IsMain { get; set; }
    }
}