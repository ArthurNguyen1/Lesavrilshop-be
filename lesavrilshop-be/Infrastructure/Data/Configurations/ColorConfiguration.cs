using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("color");

            builder.Property(c => c.ColorName)
                .IsRequired()
                .HasMaxLength(50);

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