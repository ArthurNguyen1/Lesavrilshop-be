using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.User
{
    public class UpdateUserRequestDto
    {
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public DateTime? Birthday { get; set; }
    }
}