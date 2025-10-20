using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(op => op.Product)
                   .WithMany(p => p.OrderProducts)
                   .HasForeignKey(op => op.ProductId);

        }
    }
}
