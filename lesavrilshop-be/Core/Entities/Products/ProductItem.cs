using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductItem: BaseEntity
    {
        public int ProductId { get; private set; }
        public int ColorId { get; private set; }
        public int SizeId { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public int QuantityInStock { get; private set; }
        
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public virtual SizeOption Size { get; set; }
        public virtual ICollection<ProductImage> Images { get; private set; }
        
        public ProductItem(int productId, int colorId, int sizeId, decimal originalPrice, 
            decimal salePrice, int quantityInStock)
        {
            ProductId = productId;
            ColorId = colorId;
            SizeId = sizeId;
            OriginalPrice = originalPrice;
            SalePrice = salePrice;
            QuantityInStock = quantityInStock;
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