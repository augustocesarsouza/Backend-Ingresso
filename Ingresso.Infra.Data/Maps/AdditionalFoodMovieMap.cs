using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    internal class AdditionalFoodMovieMap : IEntityTypeConfiguration<AdditionalFoodMovie>
    {
        public void Configure(EntityTypeBuilder<AdditionalFoodMovie> builder)
        {
            builder.ToTable("AdditionalFoodMovie");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title");

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price");

            builder.Property(x => x.Fee)
                .IsRequired()
                .HasColumnName("Fee");

            builder.Property(x => x.MovieId)
                .IsRequired()
                .HasColumnName("MovieId");

            builder.HasOne(x => x.Movie);
        }
    }
}
