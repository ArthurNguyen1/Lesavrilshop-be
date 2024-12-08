using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using lesavrilshop_be.Core.DTOs.Users;
using lesavrilshop_be.Core.Entities.Users;
using lesavrilshop_be.Core.Interfaces.Repositories.Users;
using lesavrilshop_be.Core.Interfaces.Services;

namespace lesavrilshop_be.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(
            IUserAddressRepository userAddressRepository,
            IAddressRepository addressRepository,
            IMapper mapper)
        {
            _userAddressRepository = userAddressRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserAddressDto>> GetUserAddressesAsync(int userId)
        {
            var userAddresses = await _userAddressRepository.GetUserAddressesWithDetailsAsync(userId);
            return _mapper.Map<IEnumerable<UserAddressDto>>(userAddresses);
        }

        public async Task<UserAddressDto> CreateUserAddressAsync(int userId, CreateUserAddressDto dto)
        {
            var address = new Address
            {
                DetailedAddress = dto.DetailedAddress,
                District = dto.District,
                City = dto.City,
                Country = dto.Country
            };

            await _addressRepository.AddAsync(address);

            var userAddress = new UserAddress
            {
                UserId = userId,
                AddressId = address.Id,
                Customer = dto.Customer,
                PhoneNumber = dto.PhoneNumber,
                IsDefault = dto.IsDefault
            };

            if (dto.IsDefault)
            {
                var existingDefault = await _userAddressRepository.GetDefaultAddressAsync(userId);
                if (existingDefault != null)
                {
                    existingDefault.IsDefault = false;
                }
            }

            await _userAddressRepository.AddAsync(userAddress);

            userAddress.Address = address;
            return _mapper.Map<UserAddressDto>(userAddress);
        }

        public async Task<bool> SetDefaultAddressAsync(int userId, int addressId)
        {
            return await _userAddressRepository.SetDefaultAddressAsync(userId, addressId);
        }

        public async Task<bool> DeleteUserAddressAsync(int userId, int addressId)
        {
            var userAddress = await _userAddressRepository.FindAsync(ua =>
                ua.UserId == userId && ua.AddressId == addressId);

            if (userAddress == null) return false;

            await _userAddressRepository.DeleteAsync(userAddress);
            return true;
        }

        public async Task<UserAddressDto?> UpdateUserAddressAsync(int userId, int addressId, UpdateUserAddressDto dto)
        {
            var userAddress = await _userAddressRepository.FindAsync(ua =>
                ua.UserId == userId && ua.AddressId == addressId);

            if (userAddress == null) return null;

            var address = await _addressRepository.GetByIdAsync(addressId);
            if (address == null) return null;

            // Only update fields that are provided
            if (dto.HasCustomer)
                userAddress.Customer = dto.Customer!;

            if (dto.HasPhoneNumber)
                userAddress.PhoneNumber = dto.PhoneNumber!;

            if (dto.HasDetailedAddress)
                address.DetailedAddress = dto.DetailedAddress!;

            if (dto.HasDistrict)
                address.District = dto.District!;

            if (dto.HasCity)
                address.City = dto.City!;

            if (dto.HasCountry)
                address.Country = dto.Country!;

            if (dto.HasIsDefault && dto.IsDefault!.Value && !userAddress.IsDefault)
            {
                var currentDefault = await _userAddressRepository.GetDefaultAddressAsync(userId);
                if (currentDefault != null)
                {
                    currentDefault.IsDefault = false;
                    await _userAddressRepository.UpdateAsync(currentDefault);
                }
                userAddress.IsDefault = true;
            }

            await _addressRepository.UpdateAsync(address);
            await _userAddressRepository.UpdateAsync(userAddress);

            var result = await _userAddressRepository.FindAsync(ua =>
                ua.UserId == userId && ua.AddressId == addressId);
            return _mapper.Map<UserAddressDto>(result);
        }
    }
}