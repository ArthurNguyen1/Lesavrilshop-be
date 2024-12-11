using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Users;

namespace lesavrilshop_be.Core.Interfaces.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<UserAddressDto>> GetUserAddressesAsync(int userId);
        Task<UserAddressDto> CreateUserAddressAsync(int userId, CreateUserAddressDto dto);
        Task<bool> SetDefaultAddressAsync(int userId, int addressId);
        Task<bool> DeleteUserAddressAsync(int userId, int addressId);
        Task<UserAddressDto?> UpdateUserAddressAsync(int userId, int addressId, UpdateUserAddressDto dto);

        Task<UserAddressDto?> GetUserAddressByIdAsync(int userId, int addressId);


    }
}