using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Users;
using lesavrilshop_be.Core.Interfaces.Repositories.Users;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Users
{
    public class UserAddressRepository : GenericRepository<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserAddress>> GetUserAddressesWithDetailsAsync(int userId)
        {
            return await _context.UserAddresses
                .Include(ua => ua.Address)
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserAddress?> GetDefaultAddressAsync(int userId)
        {
            return await _context.UserAddresses
                .Include(ua => ua.Address)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.IsDefault);
        }

        public async Task<bool> SetDefaultAddressAsync(int userId, int addressId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingDefault = await _context.UserAddresses
                    .Where(ua => ua.UserId == userId && ua.IsDefault)
                    .FirstOrDefaultAsync();

                if (existingDefault != null)
                {
                    existingDefault.IsDefault = false;
                }

                var newDefault = await _context.UserAddresses
                    .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AddressId == addressId);

                if (newDefault == null) return false;

                newDefault.IsDefault = true;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}