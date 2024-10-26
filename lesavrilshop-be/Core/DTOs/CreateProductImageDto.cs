using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs
{
    public class CreateProductImageDto
    {
        [Required]
        public int ProductItemId { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}