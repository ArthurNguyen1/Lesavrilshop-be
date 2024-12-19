using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Carts;
using lesavrilshop_be.Core.Entities.Orders;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductItem : BaseEntity
    {
        public int ProductId { get; set; }

        public int? ColorId { get; set; }
        public int? SizeId { get; set; }

        public string SKU { get; set; } = default!;


        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QuantityInStock { get; set; }


        public Product Product { get; set; } = default!;
        public Color? Color { get; set; }
        public SizeOption? Size { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}