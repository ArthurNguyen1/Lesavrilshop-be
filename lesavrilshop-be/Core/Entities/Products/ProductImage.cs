using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = default!;
        public bool IsMain { get; set; }

        public Product Product { get; set; } = default!;
    }
}