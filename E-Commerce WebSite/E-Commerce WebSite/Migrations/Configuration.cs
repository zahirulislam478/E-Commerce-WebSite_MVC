namespace E_Commerce_WebSite.Migrations
{
    using E_Commerce_WebSite.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<E_Commerce_WebSite.Models.EcommerceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(E_Commerce_WebSite.Models.EcommerceDbContext context)
        {
            // Seed Categories
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category
                {
                    Name = "Electronics",
                    Description = "The latest electronic gadgets",
                    Picture = "electronics.jpg",
                    IsActive = true,
                    Quantity = 20,
                    FeaturedDate = DateTime.Now,
                    AveragePrice = 499.99m,
                    TopSellingProduct = "Smartphone X"
                },
                new Category
                {
                    Name = "Clothing",
                    Description = "Fashionable clothing for all occasions",
                    Picture = "clothing.jpg",
                    IsActive = true,
                    Quantity = 50,
                    FeaturedDate = DateTime.Now,
                    AveragePrice = 39.99m,
                    TopSellingProduct = "Trendy T-Shirt"
                },
                new Category
                {
                    Name = "Books",
                    Description = "Best-selling books in various genres",
                    Picture = "books.jpg",
                    IsActive = true,
                    Quantity = 30,
                    FeaturedDate = DateTime.Now,
                    AveragePrice = 14.99m,
                    TopSellingProduct = "Mystery Novel"
                }
            // Add more categories as needed...
            );

            // Seed Products
            context.Products.AddOrUpdate(
                p => p.ProductName,
                new Product
                {
                    Image = "smartphoneX.jpg",
                    Brand = "TechGizmo",
                    ProductName = "Smartphone X",
                    Price = 799.99m,
                    Rating = 5,
                    CategoryId = context.Categories.First(c => c.Name == "Electronics").Id
                },
                new Product
                {
                    Image = "trendyTShirt.jpg",
                    Brand = "Fashionista",
                    ProductName = "Trendy T-Shirt",
                    Price = 29.99m,
                    Rating = 4,
                    CategoryId = context.Categories.First(c => c.Name == "Clothing").Id
                },
                new Product
                {
                    Image = "mysteryNovel.jpg",
                    Brand = "Bookworm Publications",
                    ProductName = "Mystery Novel",
                    Price = 12.99m,
                    Rating = 4,
                    CategoryId = context.Categories.First(c => c.Name == "Books").Id
                }
            // Add more products as needed...
            );
        }
    }
}
