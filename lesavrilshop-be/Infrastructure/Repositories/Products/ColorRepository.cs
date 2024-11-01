using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Products
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _context.Colors.ToListAsync();
                
        }

        public async Task<Color> GetByIdAsync(int id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public async Task<Color> CreateAsync(CreateColorDto colorDto)
        {
            var color = new Color(
                colorDto.ColorName
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();
            
            return color;
        }

        public async Task UpdateAsync(Color color)
        {
            var existingColor = await _context.Colors.FindAsync(color.Id);
            if (existingColor == null)
                throw new KeyNotFoundException($"Color with ID {color.Id} not found");

            existingColor.ColorName= color.ColorName;
            existingColor.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null)
                throw new KeyNotFoundException($"Color with ID {id} not found");

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Colors.AnyAsync(c => c.Id == id);
        }
    }
}