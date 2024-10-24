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
        public int UserId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int OrderStatusId { get; private set; }
        public string PaymentMethod { get; private set; }
        public int? CouponId { get; private set; }
        public int ShippingMethodId { get; private set; }
        public int ShippingAddressId { get; private set; }
        public decimal TotalQuantity { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string? Note { get; private set; }
        
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