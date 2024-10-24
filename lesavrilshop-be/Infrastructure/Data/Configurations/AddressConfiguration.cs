using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lesavrilshop_be.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");
            
            builder.Property(a => a.DetailedAddress)
                .IsRequired()
                .HasMaxLength(255);
                
            builder.Property(a => a.District)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}