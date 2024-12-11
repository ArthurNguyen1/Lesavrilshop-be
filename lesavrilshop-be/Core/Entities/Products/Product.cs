using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Reviews;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class Product: BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;
        
        [Required]
        public string ProductDescription { get; set; } = default!;
        
        public string? DeliveryDescription { get; set; }
        public int? ParentCategoryId { get; set; }
        public decimal RatingAverage { get; set; }
        public int RatingQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? DeletedAt { get; set; }
        
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<ProductItem> ProductItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        
        public Product(string name, string productDescription, string deliveryDescription, int? parentCategoryId = null)
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