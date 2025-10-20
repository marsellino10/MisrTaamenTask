using ecommerce.Models;

namespace ecommerce.Data.Seed
{
    public static class SeedProductExtension
    {
        public static void SeedProduct(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (context.Products.Any())
            {
                return;
            }

            // Create products
            var products = new List<Product>
            {
                new() { Name = "Wireless Headphones", Description = "Noise-cancelling Bluetooth", Price = 129.99, Stock = 50 },
                new() { Name = "Smartphone X", Description = "108MP camera phone", Price = 899.99, Stock = 25 },
                new() { Name = "Mechanical Keyboard", Description = "RGB gaming keyboard", Price = 89.99, Stock = 40 },
                new() { Name = "Yoga Mat", Description = "Non-slip exercise mat", Price = 24.99, Stock = 100 },
                new() { Name = "Electric Kettle", Description = "Fast-boiling 1.7L kettle", Price = 35.50, Stock = 30 },
                new() { Name = "Fitness Tracker", Description = "Water-resistant monitor", Price = 59.99, Stock = 75 },
                new() { Name = "Desk Lamp", Description = "Adjustable LED lamp", Price = 45.25, Stock = 60 },
                new() { Name = "Laptop Backpack", Description = "Water-resistant backpack", Price = 75.00, Stock = 45 },
                new() { Name = "Coffee Maker", Description = "Programmable 12-cup machine", Price = 69.99, Stock = 20 },
                new() { Name = "Portable Charger", Description = "10000mAh power bank", Price = 39.99, Stock = 85 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}