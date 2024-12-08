using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Users
{
    public class CreateUserAddressDto
    {
        public string Customer { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string DetailedAddress { get; set; } = default!;
        public string District { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public bool IsDefault { get; set; }
    }
}