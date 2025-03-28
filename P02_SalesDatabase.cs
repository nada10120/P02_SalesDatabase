using Microsoft.EntityFrameworkCore;
using P02_SalesDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_SalesDatabase.Data
{
    internal class P02_SalesDatabase : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EFProject513;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(p => p.Description)
                    .HasMaxLength(250)
                    .HasDefaultValue("No description");

                entity.Property(entity => entity.Description).HasDefaultValue("no description");
            });

            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity.Property(c => c.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            // Store configuration
            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(s => s.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true);
            });

            // Sale configuration
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(s => s.Date)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(s => s.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(s => s.ProductId);

                entity.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);

                entity.HasOne(s => s.Store)
                    .WithMany(st => st.Sales)
                    .HasForeignKey(s => s.StoreId);
            });
        }
    }
}
