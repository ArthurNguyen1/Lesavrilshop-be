using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Orders
{
    public class ShippingMethod: BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        
        public ShippingMethod(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}