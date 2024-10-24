using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            // Table name
            builder.ToTable("user_address");

            // Primary key
            builder.HasKey(ua => new { ua.UserId, ua.AddressId });

            // Properties
            builder.Property(ua => ua.UserId)
                .IsRequired();

            builder.Property(ua => ua.AddressId)
                .IsRequired();

            // Relationships
            builder.HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ua => ua.Address)
                .WithMany()
                .HasForeignKey(ua => ua.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}