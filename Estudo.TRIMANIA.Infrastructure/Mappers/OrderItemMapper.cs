using Estudo.TRIMANIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estudo.TRIMANIA.Infrastructure.Mappers
{
    internal class OrderItemMapper : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.ToTable(tb => tb.HasTrigger("OrderItem_UPDATE"));

            builder.HasKey(k => k.Id);

            builder.Property(p => p.DeletedAt);

            builder.Property(p => p.UpdatedAt);

            builder.Property(p => p.creation_date)
                /*.HasDefaultValueSql("getdate()")*/;

            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.product_id).IsRequired();
            builder.Property(p => p.Price).HasPrecision(15, 5);

            builder.HasOne<Order>()
                .WithMany(m => m.Items)
                .HasForeignKey(m => m.OrderId);

            builder.HasOne<Product>()
                .WithOne()
                .HasForeignKey<OrderItem>(p => p.product_id);
        }
    }
}
