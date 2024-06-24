using Estudo.TRIMANIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estudo.TRIMANIA.Infrastructure.Mappers
{
    public class AddressMapper : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.ToTable(tb => tb.HasTrigger("Address_UPDATE"));

            builder.HasKey(k => k.Id);


            builder.Property(p => p.DeletedAt);
            builder.Property(p => p.creation_date).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(p => p.UpdatedAt);

            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.State).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Street).IsRequired();
            builder.Property(p => p.Number).IsRequired();
            builder.Property(p => p.Neighborhood).IsRequired();

            builder
                .HasOne<User>()
                .WithOne(w => w.Address)
                .HasForeignKey<Address>(h => h.UserId);
        }
    }
}
