using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Entities.Carts
{
    public class CartItem: BaseEntity
    {
        public int CartId { get; private set; }
        public int ProductItemId { get; private set; }
        public int Quantity { get; private set; }
        
        public virtual Cart Cart { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        
        public CartItem(int cartId, int productItemId, int quantity)
        {
            CartId = cartId;
            ProductItemId = productItemId;
            Quantity = quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}