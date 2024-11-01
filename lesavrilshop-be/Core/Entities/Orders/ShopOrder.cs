using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Users;

namespace lesavrilshop_be.Core.Entities.Orders
{
    public class ShopOrder: BaseEntity
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
        
        public virtual ShopUser User { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; private set; }
        
        public ShopOrder(int userId, string paymentMethod, int shippingMethodId, 
            int shippingAddressId, string note = null)
        {
            UserId = userId;
            OrderDate = DateTime.UtcNow;
            OrderStatusId = 1; // Initial status
            PaymentMethod = paymentMethod;
            ShippingMethodId = shippingMethodId;
            ShippingAddressId = shippingAddressId;
            Note = note;
            TotalQuantity = 0;
            TotalPrice = 0;
            OrderItems = new List<OrderItem>();
        }
        
        public void AddOrderItem(OrderItem item)
        {
            OrderItems.Add(item);
            CalculateTotals();
        }
        
        public void UpdateStatus(int statusId)
        {
            OrderStatusId = statusId;
        }
        
        private void CalculateTotals()
        {
            TotalQuantity = OrderItems.Sum(item => item.Quantity);
            TotalPrice = OrderItems.Sum(item => item.Price * item.Quantity);
        }
    }
}