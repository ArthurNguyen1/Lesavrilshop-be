using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Reviews;
using lesavrilshop_be.Core.Entities.Reviews;
using lesavrilshop_be.Core.Interfaces.Repositories.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace lesavrilshop_be.Api.Controllers.Reviews
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(
            IReviewRepository reviewRepository,
            ILogger<ReviewController> logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving reviews");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id);
                if (review == null)
                    return NotFound();

                return Ok(review);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving review {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Review>> CreateReview(CreateReviewDto reviewDto)
        {
            try
            {
                var createdReview = await _reviewRepository.CreateAsync(reviewDto);
                
                return CreatedAtAction(
                    nameof(GetReview),
                    new { id = createdReview.Id },
                    createdReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating review");
                return StatusCode(500, new 
                { 
                    error = "Internal server error",
                    code = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.Id)
                return BadRequest();

            try
            {
                await _reviewRepository.UpdateAsync(review);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating review {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _reviewRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting review {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}