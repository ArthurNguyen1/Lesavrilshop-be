using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductItem: BaseEntity
    {
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QuantityInStock { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public virtual SizeOption Size { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        
        public ProductItem(decimal originalPrice, decimal salePrice, int quantityInStock, int? productId = null, int? colorId = null, int? sizeId = null)
        {
            OriginalPrice = originalPrice;
            SalePrice = salePrice;
            QuantityInStock = quantityInStock;
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
            Images = new List<ProductImage>();
        }

        public bool UpdateStock(int quantity)
        {
            if (QuantityInStock + quantity < 0)
                return false;
                
            QuantityInStock += quantity;
            return true;
        }

        public void UpdatePrices(decimal originalPrice, decimal salePrice)
        {
            OriginalPrice = originalPrice;
            SalePrice = salePrice;
        }
    }
}