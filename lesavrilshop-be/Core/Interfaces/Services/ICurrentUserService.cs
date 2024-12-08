using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.Interfaces.Services
{
    public interface ICurrentUserService
    {
        int GetUserId();
        string GetUserEmail();
        string GetUserRole();
    }
}