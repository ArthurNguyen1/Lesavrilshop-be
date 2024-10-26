using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs
{
    public class CreateProductCategoryDto
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}