using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories
{
    public interface IColorRepository
    {
        Task<IEnumerable<Color>> GetAllAsync();
        Task<Color> GetByIdAsync(int id);
        Task<Color> CreateAsync(CreateColorDto colorDto);
        Task UpdateAsync(Color color);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}