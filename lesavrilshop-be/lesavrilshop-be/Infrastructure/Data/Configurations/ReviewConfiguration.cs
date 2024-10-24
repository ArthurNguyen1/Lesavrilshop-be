using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("review");
            
            builder.Property(r => r.Comments)
                .HasMaxLength(1000);
                
            builder.Property(r => r.Rating)
                .IsRequired();
                
            builder.HasOne(r => r.ReviewedProduct)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ReviewedProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasIndex(r => new { r.UserId, r.ReviewedProductId })
                .IsUnique();
        }
    }
}