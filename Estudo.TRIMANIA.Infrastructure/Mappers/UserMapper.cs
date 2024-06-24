using Estudo.TRIMANIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estudo.TRIMANIA.Infrastructure.Mappers
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Birthday).IsRequired();
            builder.Property(p => p.DeletedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired();
            builder.Property(p => p.creation_date).IsRequired();

            builder.Property(p => p.CPF).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Login).IsRequired();
            builder.Property(p => p.Password).IsRequired();

            builder.Property(p => p.Identification).IsRequired();

            builder.HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<Address>(h => h.UserId)
                .IsRequired();
        }
    }
}
