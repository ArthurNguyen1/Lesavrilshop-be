using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Entities.Users;

namespace lesavrilshop_be.Core.Entities.Reviews
{
    public class Review: BaseEntity
    {
        public int UserId { get; private set; }
        public int ReviewedProductId { get; private set; }
        public string Comments { get; private set; }
        public int Rating { get; private set; }
        
        public virtual ShopUser User { get; set; }
        public virtual Product ReviewedProduct { get; set; }
        
        public Review(int userId, int reviewedProductId, string comments, int rating)
        {
            UserId = userId;
            ReviewedProductId = reviewedProductId;
            Comments = comments;
            Rating = rating;
        }
    }
}