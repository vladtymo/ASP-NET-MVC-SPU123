using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data
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
                new Product() { Id = 1, Name = "iPhone X", CategoryId = 1, Price = 650, ImageUrl="https://applecity.com.ua/image/catalog/0iphone/ipohnex/iphone-x-black.png"  },
                new Product() { Id = 2, Name = "PowerBall", CategoryId = 2, Price = 45.5M, ImageUrl="https://www.morex.lv/uploads/shop/products_original/xNi0Jqe88uyBedJJsxZw.jpg" },
                new Product() { Id = 3, Name = "Nike T-Shirt", CategoryId = 3, Price = 189, ImageUrl="https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/f4867566-14a5-42ba-8cb5-82eb65868c4c/sportswear-t-shirt-KL6HQw.png" },
                new Product() { Id = 4, Name = "Samsung S23", CategoryId = 1, Price = 1200, ImageUrl="https://content2.rozetka.com.ua/goods/images/big/310649564.jpg" }
            });
        }

        // -------------- data collection
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        //...
    }
}
