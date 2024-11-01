using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class CreateProductCategoryDto
    {
        public int? CategoryId { get; set; }

        public int? ProductId { get; set; }
    }
}