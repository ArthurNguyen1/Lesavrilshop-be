using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs
{
    public class CreateSizeOptionDto
    {
        [Required]
        public string SizeName { get; set; }
    }
}