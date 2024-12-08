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
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId)
        {
            return await _context.Addresses
                .Where(a => a.UserAddresses.Any(ua => ua.UserId == userId))
                .ToListAsync();
        }
    }
}