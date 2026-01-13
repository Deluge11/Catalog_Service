using Catalog_Service_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog_Service_Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever(); 

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.MainImage)
                .WithOne() 
                .HasForeignKey<Product>(p => p.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

        
        }

    }
}
