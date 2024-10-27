using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs
{
    public class CreateProductItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ColorId { get; set; }

        [Required]
        public int SizeId { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public int QuantityInStock { get; set; }
    }
}