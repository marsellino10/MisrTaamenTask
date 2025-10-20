using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Status)
                   .IsRequired()
                   .HasDefaultValue("Pending");

            builder.Property(o => o.OrderDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);

            builder.HasMany(o => o.OrderProducts)
                   .WithOne(op => op.Order)
                   .HasForeignKey(op => op.OrderId);
        }
    }
}
