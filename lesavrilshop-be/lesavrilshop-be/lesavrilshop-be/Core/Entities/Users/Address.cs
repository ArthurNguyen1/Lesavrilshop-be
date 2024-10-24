using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class Address: BaseEntity
    {
        public int UserId { get; private set; }
        public string DetailedAddress { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        
        public virtual ShopUser User { get; set; }
        
        public Address(int userId, string detailedAddress, string district, string city, string country)
        {
            UserId = userId;
            DetailedAddress = detailedAddress;
            District = district;
            City = city;
            Country = country;
        }
    }
}