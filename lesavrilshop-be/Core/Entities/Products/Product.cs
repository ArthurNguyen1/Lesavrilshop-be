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
        public decimal RatingAverage { get; set; }
        public int RatingQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        private string _colors;
        private string _sizes;

        public List<string> Colors
        {
            get => _colors?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
            set => _colors = string.Join(",", value);
        }

        public List<string> Sizes
        {
            get => _sizes?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
            set => _sizes = string.Join(",", value);
        }
        public DateTime? DeletedAt { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        
        public Product(string name, string productDescription, string deliveryDescription)
        {
            Name = name;
            ProductDescription = productDescription;
            DeliveryDescription = deliveryDescription;
            RatingAverage = 0;
            RatingQuantity = 0;
            Colors = new List<string>();
            Sizes = new List<string>();
            ProductCategories = new List<ProductCategory>();
            Reviews = new List<Review>();
            Images = new List<ProductImage>();
        }

        public void UpdateRating(decimal rating)
        {
            RatingAverage = ((RatingAverage * RatingQuantity) + rating) / (RatingQuantity + 1);
            RatingQuantity++;
        }
    }
}