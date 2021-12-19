using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect2.Models;

namespace Proiect2.Data
{
    public class PhoneContext : DbContext
    {
        public PhoneContext(DbContextOptions<PhoneContext> options) :base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<PhonesStore> PhonesStores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Worker>().ToTable("Worker");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Phone>().ToTable("Phone");
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<PhonesStore>().ToTable("PhonesStore");
            modelBuilder.Entity<PhonesStore>()
            .HasKey(c => new { c.PhoneID, c.StoreID });//configureaza cheiaprimara compusa
        }
    }
}
