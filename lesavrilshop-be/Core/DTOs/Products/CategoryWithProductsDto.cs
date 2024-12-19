using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public int TotalProducts { get; set; }
        public List<ProductResponseDto> Products { get; set; } = [];
    }
}