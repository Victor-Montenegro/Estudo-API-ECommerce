using Estudo.TRIMANIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estudo.TRIMANIA.Infrastructure.Mappers
{
    internal class ProductMapper : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.DeletedAt);
            builder.Property(p => p.UpdatedAt).IsRequired();
            builder.Property(p => p.creation_date).IsRequired();

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Price).HasPrecision(15,5).IsRequired();
        }
    }
}
