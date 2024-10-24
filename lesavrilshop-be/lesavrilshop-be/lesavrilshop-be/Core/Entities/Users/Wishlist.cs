using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class Wishlist: BaseEntity
    {
        public int UserId { get; private set; }
        public int ProductId { get; private set; }
        
        public virtual ShopUser User { get; set; }
        public virtual Product Product { get; set; }
        
        public Wishlist(int userId, int productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}