using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("product_item");

            builder.HasKey(pi => pi.Id);

            // Properties
            builder.Property(pi => pi.SKU)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("sku");

            builder.Property(pi => pi.OriginalPrice)
                .HasPrecision(18, 2)
                .IsRequired()
                .HasColumnName("original_price");

            builder.Property(pi => pi.SalePrice)
                .HasPrecision(18, 2)
                .IsRequired()
                .HasColumnName("sale_price");

            builder.Property(pi => pi.QuantityInStock)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnName("quantity_in_stock");

            builder.Property(pi => pi.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(pi => pi.ColorId)
                .HasColumnName("color_id");

            builder.Property(pi => pi.SizeId)
                .HasColumnName("size_id");

            // Audit properties
            builder.Property(pi => pi.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            builder.Property(pi => pi.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            // Relationships


            builder.HasOne(pi => pi.Color)
                .WithMany(c => c.ProductItems)
                .HasForeignKey(pi => pi.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pi => pi.Size)
                .WithMany(s => s.ProductItems)
                .HasForeignKey(pi => pi.SizeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}