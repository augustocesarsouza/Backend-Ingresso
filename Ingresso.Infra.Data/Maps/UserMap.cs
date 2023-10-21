using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email");

            builder.Property(x => x.EmailRecovery)
                .IsRequired()
                .HasColumnName("EmailRecovery");

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnName("Phone");

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("Cpf");

            builder.Ignore(x => x.Token);

            builder.Property(x => x.PasswordHash)
               .IsRequired()
               .HasColumnName("PasswordHash");

            builder.Property(x => x.BirthDate)
               .IsRequired()
               .HasColumnName("BirthDate");
        }
    }
}