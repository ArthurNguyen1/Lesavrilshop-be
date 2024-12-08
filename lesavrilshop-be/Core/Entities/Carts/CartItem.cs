using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Entities.Carts
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int ProductItemId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; } = default!;
        public ProductItem ProductItem { get; set; } = default!;


        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}