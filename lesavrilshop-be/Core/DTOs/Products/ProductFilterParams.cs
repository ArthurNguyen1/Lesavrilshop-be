using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lesavrilshop_be.Core.DTOs.Products
{
    public class ProductFilterParams : PaginationParams
    {
        public string? SearchTerm { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<string>? Colors { get; set; }
        public List<string>? Sizes { get; set; }
        public List<int>? CategoryIds { get; set; }
        public bool? IsActive { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductSortOption SortBy { get; set; } = ProductSortOption.Newest;
    }

    public enum ProductSortOption
    {
        Newest,
        PriceLowToHigh,
        PriceHighToLow,
        MostPopular,
        BestRating
    }
}