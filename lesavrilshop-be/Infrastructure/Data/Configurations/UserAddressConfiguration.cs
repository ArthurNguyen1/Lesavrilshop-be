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
            // Table configuration
            builder.ToTable("user_address");

            // Properties
            builder.Property(ua => ua.Customer)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ua => ua.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(ua => ua.IsDefault)
                .IsRequired()
                .HasDefaultValue(false);

            // Audit properties
            builder.Property(ua => ua.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(ua => ua.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ua => ua.Address)
                .WithMany(a => a.UserAddresses)
                .HasForeignKey(ua => ua.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure only one default address per user
            builder.HasIndex(ua => new { ua.UserId, ua.IsDefault })
                .HasFilter("\"IsDefault\" = true")
                .IsUnique();
        }
    }
}