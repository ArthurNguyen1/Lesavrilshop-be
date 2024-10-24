using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("order_status");
            
            builder.Property(os => os.Status)
                .IsRequired()
                .HasMaxLength(50);
                
            // Seed initial data
            builder.HasData(
                new OrderStatus("Pending") { Id = 1 },
                new OrderStatus("Processing") { Id = 2 },
                new OrderStatus("Shipped") { Id = 3 },
                new OrderStatus("Delivered") { Id = 4 },
                new OrderStatus("Cancelled") { Id = 5 }
            );
        }
    }
}