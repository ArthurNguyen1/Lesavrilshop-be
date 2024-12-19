using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.DTOs.Products;
using lesavrilshop_be.Core.Entities.Products;

namespace lesavrilshop_be.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> CreateProductWithImagesAsync(CreateProductDto productDto);
    }
}