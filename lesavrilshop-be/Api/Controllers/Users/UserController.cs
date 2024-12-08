using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.User;
using lesavrilshop_be.Core.DTOs.Users;
using lesavrilshop_be.Core.Extensions;
using lesavrilshop_be.Core.Interfaces.Services;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Api.Controllers.Users
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public UserController(
            ApplicationDbContext context,
            IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users
                .Include(u => u.UserAddresses)
                .FirstOrDefaultAsync(u => u.Id == long.Parse(userId));

            if (user == null)
                return NotFound("User not found");

            return Ok(user.ToDto());
        }

        [HttpPut("me")]
        public async Task<ActionResult<UserDto>> UpdateCurrentUser([FromForm] UpdateUserRequestDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
                return NotFound("User not found");

            // Handle avatar upload if provided
            if (request.AvatarFile != null)
            {
                var avatarUrl = await _imageService.UploadImageAsync(request.AvatarFile, "avatars");
                user.Avatar = avatarUrl;
            }

            // Update other properties
            if (!string.IsNullOrEmpty(request.Username))
                user.Username = request.Username;

            if (!string.IsNullOrEmpty(request.PhoneNumber))
                user.PhoneNumber = request.PhoneNumber;

            if (request.Birthday.HasValue)
                user.Birthday = request.Birthday;

            user.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(user.ToDto());
            }
            catch (DbUpdateException)
            {
                // Check if username is already taken
                if (await _context.Users.AnyAsync(u => u.Username == request.Username && u.Id != user.Id))
                    return BadRequest(new { message = "Username is already taken" });

                throw;
            }
        }

        [HttpDelete("me/avatar")]
        public async Task<ActionResult> DeleteAvatar()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _context.Users.FindAsync(long.Parse(userId));
            if (user == null)
                return NotFound("User not found");

            if (string.IsNullOrEmpty(user.Avatar))
                return BadRequest(new { message = "No avatar to delete" });

            // Delete the avatar file
            await _imageService.DeleteImageAsync(user.Avatar);

            user.Avatar = null;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Avatar deleted successfully" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublicUserDto>> GetUserById(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(new PublicUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Avatar = user.Avatar
            });
        }
    }
}