using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Users
{
    public class UpdateUserAddressDto
    {
        // Personal information
        public string? Customer { get; init; }
        public string? PhoneNumber { get; init; }

        // Address information
        public string? DetailedAddress { get; init; }
        public string? District { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }

        // Settings
        public bool? IsDefault { get; init; }

        // Check if a field is provided in the request
        public bool HasCustomer => Customer != null;
        public bool HasPhoneNumber => PhoneNumber != null;
        public bool HasDetailedAddress => DetailedAddress != null;
        public bool HasDistrict => District != null;
        public bool HasCity => City != null;
        public bool HasCountry => Country != null;
        public bool HasIsDefault => IsDefault.HasValue;
    }
}