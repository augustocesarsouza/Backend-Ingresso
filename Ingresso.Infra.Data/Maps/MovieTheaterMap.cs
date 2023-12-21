using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class MovieTheaterMap : IEntityTypeConfiguration<MovieTheater>
    {
        public void Configure(EntityTypeBuilder<MovieTheater> builder)
        {
            builder.ToTable("MovieTheater");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.MovieId)
                .IsRequired();

            builder.Property(x => x.RegionId)
                .IsRequired();

            builder.HasOne(x => x.Movie);
            builder.HasOne(x => x.Region);
        }
    }
}
