using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("Description");

            builder.Property(x => x.Gender)
                .IsRequired()
                .HasColumnName("Gender");

            builder.Property(x => x.Duration)
                .IsRequired()
                .HasColumnName("Duration");

            builder.Property(x => x.MovieRating)
                .IsRequired()
                .HasColumnName("MovieRating");

            builder.Property(x => x.PublicId)
                .IsRequired()
                .HasColumnName("PublicId");

            builder.Property(x => x.ImgUrl)
                .IsRequired()
                .HasColumnName("ImgUrl");

            builder.Property(x => x.StatusMovie)
                .IsRequired()
                .HasColumnName("StatusMovie");

            builder.Property(x => x.ImgUrlBackground)
                .IsRequired()
                .HasColumnName("ImgUrlBackground");

            builder.Property(x => x.PublicIdImgBackgound)
                .IsRequired()
                .HasColumnName("PublicIdImgBackgound");
        }
    }
}
