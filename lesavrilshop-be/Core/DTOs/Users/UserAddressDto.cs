using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Users
{
    public class UserAddressDto
    {
        public int Id { get; set; }
        public string Customer { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public bool IsDefault { get; set; }
        public AddressDto Address { get; set; } = default!;
    }
}