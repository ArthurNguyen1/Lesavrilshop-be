using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class Color : BaseEntity
    {
        public Color(string colorName)
        {
            ColorName = colorName;
        }

        [Required]
        [MaxLength(50)]
        public string ColorName { get; set; } = default!;

        public ICollection<ProductItem> ProductItems { get; set; } = [];
    }
}