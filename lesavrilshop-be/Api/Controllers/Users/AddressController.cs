using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Users;
using lesavrilshop_be.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lesavrilshop_be.Api.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ICurrentUserService _currentUserService;

        public AddressController(
            IAddressService addressService,
            ICurrentUserService currentUserService)
        {
            _addressService = addressService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAddressDto>>> GetUserAddresses()
        {
            var userId = _currentUserService.GetUserId();
            var addresses = await _addressService.GetUserAddressesAsync(userId);
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<ActionResult<UserAddressDto>> CreateAddress(CreateUserAddressDto dto)
        {
            var userId = _currentUserService.GetUserId();
            var address = await _addressService.CreateUserAddressAsync(userId, dto);
            return CreatedAtAction(nameof(GetUserAddresses), new { }, address);
        }

        [HttpPut("{addressId}/default")]
        public async Task<IActionResult> SetDefaultAddress(int addressId)
        {
            var userId = _currentUserService.GetUserId();
            var result = await _addressService.SetDefaultAddressAsync(userId, addressId);

            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            var userId = _currentUserService.GetUserId();
            var result = await _addressService.DeleteUserAddressAsync(userId, addressId);

            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult<UserAddressDto>> UpdateAddress(int addressId, UpdateUserAddressDto dto)
        {
            var userId = _currentUserService.GetUserId();
            var result = await _addressService.UpdateUserAddressAsync(userId, addressId, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<UserAddressDto>> GetAddress(int addressId)
        {
            var userId = _currentUserService.GetUserId();
            var address = await _addressService.GetUserAddressByIdAsync(userId, addressId);

            if (address == null)
                return NotFound();

            return Ok(address);
        }
    }
}