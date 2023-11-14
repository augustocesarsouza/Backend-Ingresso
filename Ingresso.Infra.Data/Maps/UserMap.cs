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
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email");

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnName("Cpf");

            builder.Property(x => x.PasswordHash)
               .IsRequired()
               .HasColumnName("PasswordHash");

            builder.Ignore(x => x.Token);

            builder.Property(x => x.ConfirmEmail)
               .IsRequired(false)
               .HasColumnName("ConfirmEmail");

            //builder.Property(x => x.Phone)
            //    .IsRequired(false)
            //    .HasColumnName("Phone");

            //builder.Property(x => x.BirthDate)
            //   .IsRequired(false)
            //   .HasColumnName("BirthDate");

        }
    }
}