using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Orders
{
    public class CreateShopOrderDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }
        public string PaymentMethod { get; set; }
        public int? CouponId { get; set; }
        public int ShippingMethodId { get; set; }
        public int ShippingAddressId { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Note { get; set; }
    }
}