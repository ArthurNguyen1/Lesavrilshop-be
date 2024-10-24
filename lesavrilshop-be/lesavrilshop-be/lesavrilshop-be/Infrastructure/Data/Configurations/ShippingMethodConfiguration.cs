using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.ToTable("shipping_method");
            
            builder.Property(sm => sm.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(sm => sm.Description)
                .HasMaxLength(255);
                
            builder.Property(sm => sm.Price)
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}