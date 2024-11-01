using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Orders
{
    public class OrderStatus: BaseEntity
    {
        public string Status { get; set; }
        
        public OrderStatus(string status)
        {
            Status = status;
        }
    }
}