using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class Wishlist : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual ShopUser User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
    }
}