using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Auth;
using OnlineShop.Dal.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Domain;
using OnlineShop.Dal.EntityConfiguration;

namespace OnlineShop.Dal
{
    public class OnlineShopDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {

        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var assembly = typeof(OrderProductsConfig).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);

            ApplyIdentityMapConfiguration(builder);
        }

        protected static void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", SchemaConstant.Auth);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaConstant.Auth);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaConstant.Auth);
            modelBuilder.Entity<UserToken>().ToTable("UserRoles", SchemaConstant.Auth);
            modelBuilder.Entity<Role>().ToTable("Roles", SchemaConstant.Auth);
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", SchemaConstant.Auth);
            modelBuilder.Entity<UserRole>().ToTable("UserRole", SchemaConstant.Auth);
        }
    }
}
