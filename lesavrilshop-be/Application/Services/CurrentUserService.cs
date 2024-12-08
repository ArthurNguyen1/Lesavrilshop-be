using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Interfaces.Services;

namespace lesavrilshop_be.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId != null ? int.Parse(userId) : 0;
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        }

        public string GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;
        }
    }
}