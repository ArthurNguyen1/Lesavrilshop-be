using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class UpdateProductImageDto
    {
        public int? Id { get; set; }
        public string ImageUrl { get; set; } = default!;
        public bool IsMain { get; set; }
        public bool IsDeleted { get; set; }
    }
}