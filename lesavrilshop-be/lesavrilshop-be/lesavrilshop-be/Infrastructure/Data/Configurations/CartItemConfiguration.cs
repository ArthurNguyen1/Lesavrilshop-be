using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
         public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_item");
            
            builder.Property(ci => ci.Quantity)
                .IsRequired()
                .HasDefaultValue(1);
                
            builder.HasOne(ci => ci.ProductItem)
                .WithMany()
                .HasForeignKey(ci => ci.ProductItemId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasIndex(ci => new { ci.CartId, ci.ProductItemId })
                .IsUnique();
        }
    }
}