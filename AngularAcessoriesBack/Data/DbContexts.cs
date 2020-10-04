using AngularAcessoriesBack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAcessoriesBack.Data
{
    public class DbContexts : IdentityDbContext<CustomIdentityUser>
    {
        public DbContexts(DbContextOptions opt): base(opt)
        {
             
        }


        public DbSet<Review> Reviews { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<CartItem> UserCart { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderProducts> OrderProducts { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<ProductFilters> ProductFilters { get; set; }

        public DbSet<Categories> categories { get; set; }

        public DbSet<CategoryValues> CategoryValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Orders>()
                .HasIndex(u => u.OrderId)
                .IsUnique();

            builder.Entity<Categories>()
                .HasIndex(u => u.Category)
                .IsUnique();

            builder.Entity<CategoryValues>()
                .HasIndex(u => u.Value)
                .IsUnique();

            builder.Entity<CategoryValues>()
                .HasOne(cv => cv.category)
                .WithMany(c => c.values)
                .HasForeignKey(cv => cv.Category)
                .HasPrincipalKey(c => c.Category);

            builder.Entity<ProductFilters>()
                .HasOne(PF => PF.category)
                .WithMany(C => C.productFilters)
                .HasForeignKey(PF => PF.Category)
                .HasPrincipalKey(c => c.Category);


            builder.Entity<ProductFiltersValues>()
                .HasOne<ProductFilters>(PF => PF.productFilters)
                .WithMany(PFV => PFV.productFiltersValues)
                .HasForeignKey(PF => PF.productFilterId)
                .HasPrincipalKey(PFV => PFV.productId);

            builder.Entity<ProductFiltersValues>()
                .HasOne<CategoryValues>(cv => cv.categoryValues)
                .WithMany(PFV => PFV.productFiltersValues)
                .HasForeignKey(PF => PF.Value)
                .HasPrincipalKey(cv => cv.Value);        
        }
    }
}
