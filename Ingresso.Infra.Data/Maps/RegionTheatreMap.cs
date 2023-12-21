using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class RegionTheatreMap : IEntityTypeConfiguration<RegionTheatre>
    {
        public void Configure(EntityTypeBuilder<RegionTheatre> builder)
        {
            builder.ToTable("RegionTheatre");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .HasColumnName("Id");

            builder.Property(x => x.TheatreId)
                .IsRequired()
                .HasColumnName("TheatreId");

            builder.Property(x => x.RegionId)
                .IsRequired()
                .HasColumnName("RegionId");

            builder.HasOne(x => x.Theatre);
            builder.HasOne(x => x.Region);
        }
    }
}
