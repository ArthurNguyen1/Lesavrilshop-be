using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class SizeOption : BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string SizeName { get; set; } = default!;

        public ICollection<ProductItem> ProductItems { get; set; } = [];

        public SizeOption() { } // Default constructor

        public SizeOption(string sizeName)
        {
            SizeName = sizeName;
        }
    }
}