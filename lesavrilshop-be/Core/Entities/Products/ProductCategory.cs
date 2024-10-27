using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductCategory: BaseEntity
    {
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
        
        public ProductCategory(int? categoryId = null, int? productId = null)
        {
            CategoryId = categoryId;
            ProductId = productId;
        }
    }
}