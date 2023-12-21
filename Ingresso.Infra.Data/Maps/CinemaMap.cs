using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class CinemaMap : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.ToTable("Cinema");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.NameCinema)
                .IsRequired()
                .HasColumnName("NameCinema");

            builder.Property(x => x.District)
                .IsRequired()
                .HasColumnName("District");

            builder.Property(x => x.Ranking)
                .IsRequired()
                .HasColumnName("Ranking");
        }
    }
}
