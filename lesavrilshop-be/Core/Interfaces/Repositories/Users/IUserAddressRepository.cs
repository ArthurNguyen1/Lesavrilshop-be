using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Users;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Users
{
    public interface IUserAddressRepository : IGenericRepository<UserAddress>
    {
        Task<IEnumerable<UserAddress>> GetUserAddressesWithDetailsAsync(int userId);
        Task<UserAddress?> GetDefaultAddressAsync(int userId);
        Task<bool> SetDefaultAddressAsync(int userId, int addressId);
    }
}