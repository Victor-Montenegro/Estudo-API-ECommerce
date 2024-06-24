using Estudo.TRIMANIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estudo.TRIMANIA.Infrastructure.Mappers
{
    internal class OrderMapper : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.UpdatedAt).IsRequired();
            builder.Property(p => p.DeletedAt).IsRequired();
            builder.Property(p => p.creation_date).IsRequired();

            builder.Property(p => p.cancel_date);
            builder.Property(p => p.finished_date);
            builder.Property(p => p.total_value).IsRequired().HasPrecision(15, 5);
            builder.Property(p => p.Status).IsRequired();

            builder.HasMany(m => m.Items)
                .WithOne()
                .HasForeignKey(f => f.OrderId);

            builder.HasOne(h => h.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);
        }
    }
}
