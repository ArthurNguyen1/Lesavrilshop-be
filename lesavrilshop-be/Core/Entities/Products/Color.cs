using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;

namespace lesavrilshop_be.Core.Entities.Products
{
    public class Color: BaseEntity
    {
        public string ColorName { get; private set; }
        
        public Color(string colorName)
        {
            ColorName = colorName;
        }
    }
}