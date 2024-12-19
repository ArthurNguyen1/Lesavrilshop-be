using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductImage: BaseEntity
    {
        public string ImageUrl { get; set; }
        public int? ProductItemId { get; set; }

        public virtual ProductItem ProductItem { get; set; }
        
        public ProductImage(string imageUrl, int? productItemId = null)
        {
            ImageUrl = imageUrl;
            ProductItemId = productItemId;
        }
    }
}