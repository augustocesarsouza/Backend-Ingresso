using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class CinemaMovieMap : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.ToTable("CinemaMovie");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.ScreeningSchedule)
                .IsRequired()
                .HasColumnName("ScreeningSchedule");

            builder.Property(x => x.CinemaId)
                .IsRequired()
                .HasColumnName("CinemaId");

            builder.Property(x => x.MovieId)
               .IsRequired()
               .HasColumnName("MovieId");

            builder.Property(x => x.RegionId)
               .IsRequired()
               .HasColumnName("RegionId");

            builder.HasOne(x => x.Cinema);
            builder.HasOne(x => x.Movie);
            builder.HasOne(x => x.Region);
        }
    }
}
