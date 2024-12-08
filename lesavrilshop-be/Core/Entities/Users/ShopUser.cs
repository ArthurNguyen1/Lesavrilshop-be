using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Carts;
using lesavrilshop_be.Core.Entities.Orders;
using lesavrilshop_be.Core.Entities.Reviews;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class ShopUser : BaseEntity
    {
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
        public string Username { get; set; } = default!;
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = "Customer";

        public ICollection<UserAddress> UserAddresses { get; set; } = [];
        public ICollection<ShopOrder> Orders { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public Cart? Cart { get; set; }
    }
}