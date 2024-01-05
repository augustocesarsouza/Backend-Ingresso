using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class FormOfPaymentMap : IEntityTypeConfiguration<FormOfPayment>
    {
        public void Configure(EntityTypeBuilder<FormOfPayment> builder)
        {
            builder.ToTable("FormOfPayment");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.FormName)
                .IsRequired()
                .HasColumnName("FormName");

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price");

            builder.Property(x => x.MovieId)
                .IsRequired()
                .HasColumnName("MovieId");

            builder.HasOne(x => x.Movie);
        }
    }
}
