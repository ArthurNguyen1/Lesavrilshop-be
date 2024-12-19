using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class ProductCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public Category Category { get; set; } = default!;
        public Product Product { get; set; } = default!;

        public ProductCategory() { } // Add default constructor


    }
}