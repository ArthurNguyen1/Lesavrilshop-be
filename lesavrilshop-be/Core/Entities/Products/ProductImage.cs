using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductImage: BaseEntity
    {
        public int? ProductItemId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = default!;
        public bool IsMain { get; set; }
        
        public virtual ProductItem ProductItem { get; set; }
        
        public ProductImage(string imageUrl, bool isMain, int? productItemId = null)
        {
            ImageUrl = imageUrl;
            IsMain = isMain;
            ProductItemId = productItemId;
        }
    }
}