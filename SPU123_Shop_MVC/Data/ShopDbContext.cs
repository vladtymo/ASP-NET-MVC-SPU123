﻿using Microsoft.EntityFrameworkCore;
using SPU123_Shop_MVC.Entities;

namespace SPU123_Shop_MVC.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //string str = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SPU123_shop_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //optionsBuilder.UseSqlServer(str);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new[]
            {
                new Category() { Id = 1, Name = "Electronics" },
                new Category() { Id = 2, Name = "Sport" },
                new Category() { Id = 3, Name = "Fashion" },
                new Category() { Id = 4, Name = "Home & Garden" },
                new Category() { Id = 5, Name = "Transport" },
                new Category() { Id = 6, Name = "Toys & Hobbies" },
                new Category() { Id = 7, Name = "Musical Instruments" },
                new Category() { Id = 8, Name = "Art" }
            });

            modelBuilder.Entity<Product>().HasData(new[]
            {
                new Product() { Id = 1, Name = "iPhone X", CategoryId = 1, Price = 650 },
                new Product() { Id = 2, Name = "PowerBall", CategoryId = 2, Price = 45.5M },
                new Product() { Id = 3, Name = "Nike T-Shirt", CategoryId = 3, Price = 189 },
                new Product() { Id = 4, Name = "Samsung S23", CategoryId = 1, Price = 1200 }
            });
        }

        // -------------- data collection
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        //...
    }
}