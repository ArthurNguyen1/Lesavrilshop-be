using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("product_image");

            // Properties
            builder.Property(pi => pi.ImageUrl)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("image_url");

            builder.Property(pi => pi.IsMain)
                .HasDefaultValue(false)
                .HasColumnName("is_main");

            builder.Property(pi => pi.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            // Audit properties
            builder.Property(pi => pi.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            builder.Property(pi => pi.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            // Relationship
            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}