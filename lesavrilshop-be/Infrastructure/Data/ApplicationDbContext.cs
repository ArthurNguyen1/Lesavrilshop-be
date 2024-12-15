using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lesavrilshop_be.Core.Common;
using lesavrilshop_be.Core.Entities.Carts;
using lesavrilshop_be.Core.Entities.Orders;
using lesavrilshop_be.Core.Entities.Products;
using lesavrilshop_be.Core.Entities.Reviews;
using lesavrilshop_be.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace lesavrilshop_be.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<SizeOption> SizeOptions { get; set; }
        public DbSet<ShopOrder> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<ShopUser> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Global query filters
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => p.DeletedAt == null);

            modelBuilder.Entity<ShopUser>()
                .HasQueryFilter(u => u.IsActive);

            // Configure Product entity: Colors and Sizes as List<string>
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Colors)
                    .HasColumnType("text") // Adjust for your database provider
                    .HasConversion(
                        v => string.Join(",", v), // Serialize List<string> to a comma-separated string
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Deserialize back to List<string>
                    );

                entity.Property(p => p.Sizes)
                    .HasColumnType("text") // Adjust for your database provider
                    .HasConversion(
                        v => string.Join(",", v), // Serialize List<string> to a comma-separated string
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() // Deserialize back to List<string>
                    );
            });

            // Add created_at and updated_at triggers for PostgreSQL
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.CreatedAt))
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.UpdatedAt))
                        .HasDefaultValueSql("CURRENT_TIMESTAMP")
                        .ValueGeneratedOnAddOrUpdate();
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (BaseEntity)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }

                entity.UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}