using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = default!;
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}