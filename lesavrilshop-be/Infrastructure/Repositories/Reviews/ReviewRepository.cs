using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Reviews;
using lesavrilshop_be.Core.Entities.Reviews;
using lesavrilshop_be.Core.Interfaces.Repositories.Reviews;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
                
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<Review> CreateAsync(CreateReviewDto reviewDto)
        {
            var review = new Review(
                reviewDto.Comments,
                reviewDto.Rating,
                reviewDto.UserId,
                reviewDto.ReviewedProductId
            )
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            
            return review;
        }

        public async Task UpdateAsync(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null)
                throw new KeyNotFoundException($"Review with ID {review.Id} not found");

            existingReview.UserId= review.UserId;
            existingReview.ReviewedProductId= review.ReviewedProductId;
            existingReview.Comments= review.Comments;
            existingReview.Rating= review.Rating;

            existingReview.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {id} not found");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }
    }
}