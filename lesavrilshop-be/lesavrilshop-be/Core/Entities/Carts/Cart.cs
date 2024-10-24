using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Users;

namespace lesavrilshop_be.Core.Entities.Carts
{
    public class Cart: BaseEntity
    {
        public int UserId { get; private set; }
        
        public virtual ShopUser User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; private set; }
        
        public Cart(int userId)
        {
            UserId = userId;
            CartItems = new List<CartItem>();
        }
    }
}