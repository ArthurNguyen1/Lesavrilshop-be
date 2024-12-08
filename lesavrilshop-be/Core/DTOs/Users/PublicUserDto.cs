using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Users
{
    public class PublicUserDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string? Avatar { get; set; }

    }
}