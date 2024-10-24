using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class Category: BaseEntity
    {
        public string Name { get;  set; }
        public string Image { get; set; }
        public int? ParentCategoryId { get;  set; }
        
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Subcategories { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        
        public Category(string name, string image, int? parentCategoryId = null)
        {
            Name = name;
            Image = image;
            ParentCategoryId = parentCategoryId;
            Subcategories = new List<Category>();
            ProductCategories = new List<ProductCategory>();
        }
    }
}