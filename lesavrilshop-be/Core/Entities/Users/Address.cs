using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class Address : BaseEntity
    {
        public int UserId { get; set; }
        public string DetailedAddress { get; set; } = default!;
        public string District { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;

        public ICollection<UserAddress> UserAddresses { get; set; } = [];
    }
}