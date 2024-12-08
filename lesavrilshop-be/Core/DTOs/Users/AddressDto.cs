using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Users
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string DetailedAddress { get; set; } = default!;
        public string District { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}