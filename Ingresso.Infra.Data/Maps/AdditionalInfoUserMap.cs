using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class AdditionalInfoUserMap : IEntityTypeConfiguration<AdditionalInfoUser>
    {
        public void Configure(EntityTypeBuilder<AdditionalInfoUser> builder)
        {
            builder.ToTable("AdditionalInfoUser");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.BirthDate)
                .IsRequired(false)
                .HasColumnName("BirthDate");

            builder.Property(x => x.Gender)
                .IsRequired(false)
                .HasColumnName("Gender");

            builder.Property(x => x.Phone)
                .IsRequired(false)
                .HasColumnName("Phone");

            builder.Property(x => x.Cep)
                .IsRequired(false)
                .HasColumnName("Cep");

            builder.Property(x => x.Logradouro)
                .IsRequired(false)
                .HasColumnName("Logradouro");

            builder.Property(x => x.Numero)
                .IsRequired(false)
                .HasColumnName("Numero");

            builder.Property(x => x.Complemento)
                .IsRequired(false)
                .HasColumnName("Complemento");

            builder.Property(x => x.Referencia)
                .IsRequired(false)
                .HasColumnName("Referencia");

            builder.Property(x => x.Bairro)
                .IsRequired(false)
                .HasColumnName("Bairro");

            builder.Property(x => x.Estado)
                .IsRequired(false)
                .HasColumnName("Estado");

            builder.Property(x => x.Cidade)
                .IsRequired(false)
                .HasColumnName("Cidade");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("UserId");

            builder.HasOne(x => x.User);
        }
    }
}
