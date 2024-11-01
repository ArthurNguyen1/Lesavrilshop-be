using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Reviews
{
    public class CreateReviewDto
    {
        [Required]
        public string Comments { get; set; }
        [Required]
        public int Rating { get; set; }
        public int? UserId { get; set; }
        public int? ReviewedProductId { get; set; }
    }
}