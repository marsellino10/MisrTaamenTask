using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Email)
                   .IsRequired();

            builder.HasIndex(c => c.Email)
                   .IsUnique();

            builder.Property(c => c.Phone)
                   .IsRequired()
                   .HasMaxLength(15);
            
        }
    }
}
