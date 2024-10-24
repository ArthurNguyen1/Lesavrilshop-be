using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ProductItemConfiguration: IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("product_item");
            
            builder.Property(pi => pi.OriginalPrice)
                .HasPrecision(10, 2);
                
            builder.Property(pi => pi.SalePrice)
                .HasPrecision(10, 2);
                
            builder.HasIndex(pi => new { pi.ProductId, pi.ColorId, pi.SizeId })
                .IsUnique();
        }
    }
}