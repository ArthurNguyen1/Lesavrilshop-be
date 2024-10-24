using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
         public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");
            
            builder.Property(oi => oi.Price)
                .HasPrecision(10, 2)
                .IsRequired();
                
            builder.Property(oi => oi.Quantity)
                .IsRequired();
                
            builder.HasOne(oi => oi.ProductItem)
                .WithMany()
                .HasForeignKey(oi => oi.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}