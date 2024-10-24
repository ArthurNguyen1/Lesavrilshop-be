using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Carts;
using lesavrilshop_be.Core.Entities.Orders;

namespace lesavrilshop_be.Core.Entities.Users
{
    public class ShopUser: BaseEntity
    {
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Username { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string Avatar { get; private set; }
        public bool IsActive { get; private set; }
        public string Role { get; private set; }
        
        public virtual ICollection<Address> Addresses { get; private set; }
        public virtual ICollection<ShopOrder> Orders { get; private set; }
        public virtual Cart Cart { get; private set; }
        
        public ShopUser(string email, string password, string username, string phoneNumber)
        {
            Email = email;
            Password = password;
            Username = username;
            PhoneNumber = phoneNumber;
            IsActive = true;
            Role = "Customer";
            Addresses = new List<Address>();
            Orders = new List<ShopOrder>();
        }

        public void UpdateProfile(string username, DateTime? birthday, string avatar)
        {
            Username = username;
            Birthday = birthday;
            Avatar = avatar;
        }
    }
}