using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Entities.Orders
{
    public class OrderItem: BaseEntity
    {
        public int ProductItemId { get; private set; }
        public int OrderId { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        
        public virtual ProductItem ProductItem { get; set; }
        public virtual ShopOrder Order { get; set; }
        
        public OrderItem(int productItemId, int orderId, decimal price, int quantity)
        {
            ProductItemId = productItemId;
            OrderId = orderId;
            Price = price;
            Quantity = quantity;
        }
    }
}