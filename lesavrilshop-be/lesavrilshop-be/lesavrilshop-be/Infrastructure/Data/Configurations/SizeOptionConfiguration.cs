using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class SizeOptionConfiguration : IEntityTypeConfiguration<SizeOption>
    {
        public void Configure(EntityTypeBuilder<SizeOption> builder)
        {
            builder.ToTable("size_option");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SizeName)
                .IsRequired()
                .HasMaxLength(20);

        }
    }
}