using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Orders
{
    public class CreateOrderStatusDto
    {
        [Required]
        public string Status { get; set; }

    }
}