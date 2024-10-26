using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Repositories
{
    public interface ISizeOptionRepository
    {
        Task<IEnumerable<SizeOption>> GetAllAsync();
        Task<SizeOption> GetByIdAsync(int id);
        Task<SizeOption> CreateAsync(CreateSizeOptionDto sizeOptionDto);
        Task UpdateAsync(SizeOption sizeOption);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}