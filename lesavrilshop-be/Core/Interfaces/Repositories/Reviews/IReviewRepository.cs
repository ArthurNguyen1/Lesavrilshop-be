using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Reviews;
using lesavrilshop_be.Core.Entities.Reviews;

namespace lesavrilshop_be.Core.Interfaces.Repositories.Reviews
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task<Review> CreateAsync(CreateReviewDto reviewDto);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}