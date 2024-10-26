using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class SizeOption: BaseEntity
    {
        public string SizeName { get; set; }
        
        public SizeOption(string sizeName)
        {
            SizeName = sizeName;
        }
    }
}