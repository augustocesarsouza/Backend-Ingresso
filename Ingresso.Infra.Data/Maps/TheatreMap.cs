using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class TheatreMap : IEntityTypeConfiguration<Theatre>
    {
        public void Configure(EntityTypeBuilder<Theatre> builder)
        {
            builder.ToTable("Theatre");
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

            builder.Property(x => x.Data)
                .IsRequired()
                .HasColumnName("Data");

            builder.Property(x => x.Location)
                .IsRequired()
                .HasColumnName("Location");

            builder.Property(x => x.TypeOfAttraction)
                .IsRequired()
                .HasColumnName("TypeOfAttraction");

            builder.Property(x => x.Category)
                .IsRequired()
                .HasColumnName("Category");

            builder.Property(x => x.PublicId)
                .IsRequired()
                .HasColumnName("PublicId");

            builder.Property(x => x.ImgUrl)
                .IsRequired()
                .HasColumnName("ImgUrl");
        }
    }
}
