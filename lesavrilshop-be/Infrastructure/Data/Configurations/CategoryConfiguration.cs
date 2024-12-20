using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);


            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Audit properties
            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(c => c.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");
        }
    }
}