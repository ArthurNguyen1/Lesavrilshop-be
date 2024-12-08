using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class UserAddress : BaseEntity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }

        public string Customer { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public bool IsDefault { get; set; }


        public ShopUser User { get; set; } = default!;
        public Address Address { get; set; } = default!;

    }
}