using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Reviews;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class Product: BaseEntity
    {
        public string Name { get; private set; }
        public string ProductDescription { get; private set; }
        public string DeliveryDescription { get; private set; }
        public int ParentCategoryId { get; private set; }
        public decimal RatingAverage { get; private set; }
        public int RatingQuantity { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get; private set; }
        public virtual ICollection<Review> Reviews { get; private set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; private set; }
        
        public Product(string name, string productDescription, string deliveryDescription, int parentCategoryId)
        {
            Name = name;
            ProductDescription = productDescription;
            DeliveryDescription = deliveryDescription;
            ParentCategoryId = parentCategoryId;
            RatingAverage = 0;
            RatingQuantity = 0;
            ProductItems = new List<ProductItem>();
            Reviews = new List<Review>();
            ProductCategories = new List<ProductCategory>();
        }

        public void UpdateRating(decimal rating)
        {
            RatingAverage = ((RatingAverage * RatingQuantity) + rating) / (RatingQuantity + 1);
            RatingQuantity++;
        }
    }
}