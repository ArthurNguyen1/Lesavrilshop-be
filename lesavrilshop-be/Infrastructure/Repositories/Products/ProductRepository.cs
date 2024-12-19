using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Interfaces.Repositories.Products;
using lesavrilshop_be.Core.Interfaces.Services;
using lesavrilshop_be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(
            ApplicationDbContext context,
            ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PaginatedResult<Product>> GetAllAsync(ProductFilterParams filterParams)
        {
            var query = _context.Products.AsQueryable();

            // Apply base filters
            query = ApplyBaseFilters(query, filterParams);

            // Get products with includes
            var products = await query
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .ToListAsync();

            // Apply colors and sizes filtering
            var filteredProducts = ApplyColorAndSizeFilters(products, filterParams);

            // Apply sorting
            filteredProducts = ApplySorting(filteredProducts, filterParams.SortBy);

            // Get total count after all filters
            var totalItems = filteredProducts.Count;

            // Apply pagination
            var items = ApplyPagination(filteredProducts, filterParams);

            return CreatePaginatedResult(items, filterParams, totalItems);
        }

        private static List<Product> ApplyColorAndSizeFilters(List<Product> products, ProductFilterParams filterParams)
        {
            var filteredProducts = products;

            if (filterParams.Colors?.Any() == true)
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Colors.Any(productColor =>
                        filterParams.Colors.Any(filterColor =>
                            productColor.Equals(filterColor, StringComparison.OrdinalIgnoreCase))))
                    .ToList();
            }

            if (filterParams.Sizes?.Any() == true)
            {
                filteredProducts = filteredProducts
                    .Where(p => p.Sizes.Any(productSize =>
                        filterParams.Sizes.Any(filterSize =>
                            productSize.Equals(filterSize, StringComparison.OrdinalIgnoreCase))))
                    .ToList();
            }

            return filteredProducts;
        }

        public async Task<PaginatedResult<Product>> GetByCategoryAsync(
            int categoryId,
            ProductFilterParams filterParams)
        {
            // Get the category and its subcategories
            var categoryWithSubcategories = await _context.Categories
                .Where(c => c.Id == categoryId)
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync();

            if (categoryWithSubcategories == null)
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");

            // Collect all category IDs (main category and its subcategories)
            var categoryIds = new List<int> { categoryId };
            categoryIds.AddRange(categoryWithSubcategories.Subcategories.Select(sc => sc.Id));

            // Start with base query
            var query = _context.Products
                .Where(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryId)));

            // Apply base filters
            query = ApplyBaseFilters(query, filterParams);

            // Get products with includes
            var products = await query
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .ToListAsync();

            // Apply colors and sizes filtering
            var filteredProducts = ApplyColorAndSizeFilters(products, filterParams);

            // Apply sorting
            filteredProducts = ApplySorting(filteredProducts, filterParams.SortBy);

            // Get total count after all filters
            var totalItems = filteredProducts.Count;

            // Apply pagination
            var items = ApplyPagination(filteredProducts, filterParams);

            return CreatePaginatedResult(items, filterParams, totalItems);
        }

        public async Task<PaginatedResult<Product>> SearchAsync(ProductFilterParams filterParams)
        {
            // If no search term provided, return all products
            if (string.IsNullOrWhiteSpace(filterParams.SearchTerm))
                return await GetAllAsync(filterParams);

            var query = _context.Products.AsQueryable();

            // Apply search term
            var searchTerm = filterParams.SearchTerm.ToLower().Trim();
            query = query.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.ProductDescription.ToLower().Contains(searchTerm) ||
                p.ProductCategories.Any(pc => pc.Category.Name.ToLower().Contains(searchTerm)));

            // Apply base filters
            query = ApplyBaseFilters(query, filterParams);

            // Get products with includes
            var products = await query
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .ToListAsync();

            // Apply colors and sizes filtering
            var filteredProducts = ApplyColorAndSizeFilters(products, filterParams);

            // Apply sorting
            filteredProducts = ApplySorting(filteredProducts, filterParams.SortBy);

            // Get total count after all filters
            var totalItems = filteredProducts.Count;

            // Apply pagination
            var items = ApplyPagination(filteredProducts, filterParams);

            return CreatePaginatedResult(items, filterParams, totalItems);
        }

        private static IQueryable<Product> ApplyBaseFilters(
            IQueryable<Product> query,
            ProductFilterParams filterParams)
        {
            // Filter by active status
            if (filterParams.IsActive.HasValue)
                query = query.Where(p => p.IsActive == filterParams.IsActive.Value);

            // Filter by price range
            if (filterParams.MinPrice.HasValue)
                query = query.Where(p => p.SalePrice >= filterParams.MinPrice.Value);
            if (filterParams.MaxPrice.HasValue)
                query = query.Where(p => p.SalePrice <= filterParams.MaxPrice.Value);

            // Filter by categories
            if (filterParams.CategoryIds?.Any() == true)
                query = query.Where(p => p.ProductCategories
                    .Any(pc => filterParams.CategoryIds.Contains(pc.CategoryId)));

            // Apply search term if provided
            if (!string.IsNullOrWhiteSpace(filterParams.SearchTerm))
            {
                var searchTerm = filterParams.SearchTerm.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.ProductDescription.ToLower().Contains(searchTerm) ||
                    p.ProductCategories.Any(pc => pc.Category.Name.ToLower().Contains(searchTerm)));
            }

            return query;
        }

        private static List<Product> ApplySorting(
            List<Product> products,
            ProductSortOption sortBy)
        {
            return sortBy switch
            {
                ProductSortOption.PriceLowToHigh => products.OrderBy(p => p.SalePrice).ToList(),
                ProductSortOption.PriceHighToLow => products.OrderByDescending(p => p.SalePrice).ToList(),
                ProductSortOption.MostPopular => products.OrderByDescending(p => p.RatingQuantity).ToList(),
                ProductSortOption.BestRating => products.OrderByDescending(p => p.RatingAverage).ToList(),
                _ => products.OrderByDescending(p => p.CreatedAt).ToList() // ProductSortOption.Newest
            };
        }

        private static List<Product> ApplyPagination(
            List<Product> products,
            PaginationParams paginationParams)
        {
            return products
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToList();
        }

        private static PaginatedResult<Product> CreatePaginatedResult(
            List<Product> items,
            PaginationParams paginationParams,
            int totalItems)
        {
            return new PaginatedResult<Product>
            {
                Items = items,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)paginationParams.PageSize)
            };
        }

        public async Task<Product?> GetByIdAsync(int id, bool includeInactive = false)
        {
            return await _context.Products
                .Where(p => p.Id == id && (includeInactive || p.IsActive))
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(CreateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                ProductDescription = productDto.ProductDescription,
                DeliveryDescription = productDto.DeliveryDescription,
                OriginalPrice = productDto.OriginalPrice,
                SalePrice = productDto.SalePrice,
                QuantityInStock = productDto.QuantityInStock,
                Colors = productDto.Colors ?? new List<string>(),
                Sizes = productDto.Sizes ?? new List<string>(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (productDto.CategoryIds?.Any() == true)
            {
                product.ProductCategories = productDto.CategoryIds.Select(categoryId =>
                    new ProductCategory
                    {
                        CategoryId = categoryId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }).ToList();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Reload the product with all navigation properties
            return await GetByIdAsync(product.Id, true) ??
                throw new InvalidOperationException("Failed to reload created product");
        }

        public async Task UpdateAsync(int id, UpdateProductDto productDto)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            // Update basic properties
            product.Name = productDto.Name;
            product.ProductDescription = productDto.ProductDescription;
            product.DeliveryDescription = productDto.DeliveryDescription;
            product.OriginalPrice = productDto.OriginalPrice;
            product.SalePrice = productDto.SalePrice;
            product.QuantityInStock = productDto.QuantityInStock;
            product.Colors = productDto.Colors ?? new List<string>();
            product.Sizes = productDto.Sizes ?? new List<string>();
            product.IsActive = productDto.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            // Update categories if provided
            if (productDto.CategoryIds != null)
            {
                // Remove existing categories
                _context.ProductCategories.RemoveRange(product.ProductCategories);

                // Add new categories
                product.ProductCategories = productDto.CategoryIds.Select(categoryId =>
                    new ProductCategory
                    {
                        CategoryId = categoryId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }).ToList();
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }


    }
}
