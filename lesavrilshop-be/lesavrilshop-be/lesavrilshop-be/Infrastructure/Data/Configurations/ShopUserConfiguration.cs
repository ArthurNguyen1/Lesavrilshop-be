using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Carts;
using lesavrilshop_be.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class ShopUserConfiguration : IEntityTypeConfiguration<ShopUser>
    {
        public void Configure(EntityTypeBuilder<ShopUser> builder)
        {
            builder.ToTable("shop_user");
            
            builder.HasIndex(u => u.Email)
                .IsUnique();
                
            builder.HasIndex(u => u.PhoneNumber)
                .IsUnique();
                
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
                
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
                
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Customer");

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}