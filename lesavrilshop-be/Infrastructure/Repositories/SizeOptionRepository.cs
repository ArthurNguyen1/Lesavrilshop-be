using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories
{
    public class SizeOptionRepository : ISizeOptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SizeOptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SizeOption>> GetAllAsync()
        {
            return await _context.SizeOptions.ToListAsync();
                
        }

        public async Task<SizeOption> GetByIdAsync(int id)
        {
            return await _context.SizeOptions.FindAsync(id);
        }

        public async Task<SizeOption> CreateAsync(CreateSizeOptionDto sizeOptionDto)
        {
            var sizeOption = new SizeOption(
                sizeOptionDto.SizeName
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.SizeOptions.Add(sizeOption);
            await _context.SaveChangesAsync();
            
            return sizeOption;
        }

        public async Task UpdateAsync(SizeOption sizeOption)
        {
            var existingSizeOption = await _context.SizeOptions.FindAsync(sizeOption.Id);
            if (existingSizeOption == null)
                throw new KeyNotFoundException($"SizeOption with ID {sizeOption.Id} not found");

            existingSizeOption.SizeName= sizeOption.SizeName;
            existingSizeOption.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sizeOption = await _context.SizeOptions.FindAsync(id);
            if (sizeOption == null)
                throw new KeyNotFoundException($"SizeOption with ID {id} not found");

            _context.SizeOptions.Remove(sizeOption);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.SizeOptions.AnyAsync(s => s.Id == id);
        }
    }
}