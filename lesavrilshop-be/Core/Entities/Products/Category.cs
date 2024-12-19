using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class Category : BaseEntity
    {
        public Category(string name, int? parentCategoryId)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;
        public int? ParentCategoryId { get; set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { get; set; }
        public ICollection<Category> Subcategories { get; set; } = [];
        public ICollection<ProductCategory> ProductCategories { get; set; } = [];
    }
}