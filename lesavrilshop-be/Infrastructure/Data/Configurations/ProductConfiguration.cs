using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);
                
            builder.Property(p => p.ProductDescription)
                .IsRequired();
                
            builder.Property(p => p.RatingAverage)
                .HasPrecision(3, 2);
                
            builder.HasMany(p => p.ProductItems)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}