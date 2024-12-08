using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.User;
using lesavrilshop_be.Core.Entities.Users;

namespace lesavrilshop_be.Core.Extensions
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this ShopUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Avatar = user.Avatar,
                IsActive = user.IsActive,
                Birthday = user.Birthday,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}