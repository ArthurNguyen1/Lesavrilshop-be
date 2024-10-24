using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class UserAddress: BaseEntity
    {
        public int UserId { get; private set; }
        public int AddressId { get; private set; }
        
        public virtual ShopUser User { get; set; }
        public virtual Address Address { get; set; }
        
        public UserAddress(int userId, int addressId)
        {
            UserId = userId;
            AddressId = addressId;
        }
    }
}