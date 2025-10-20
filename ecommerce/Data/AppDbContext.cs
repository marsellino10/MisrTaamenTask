using ecommerce.Configurations;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ECommerceDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        }
    }
}
