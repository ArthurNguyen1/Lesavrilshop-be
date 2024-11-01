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
        public string Comments { get; set; }
        public int Rating { get; set; }
        public int? UserId { get; set; }
        public int? ReviewedProductId { get; set; }
        
        public virtual ShopUser User { get; set; }
        public virtual Product ReviewedProduct { get; set; }
        
        public Review(string comments, int rating, int? userId = null, int? reviewedProductId = null)
        {           
            Comments = comments;
            Rating = rating;
            UserId = userId;
            ReviewedProductId = reviewedProductId;
        }
    }
}