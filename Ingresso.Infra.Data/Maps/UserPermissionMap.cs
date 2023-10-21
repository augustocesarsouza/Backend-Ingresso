using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ingresso.Infra.Data.Maps
{
    public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermission");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            builder.Property(x => x.PermissionId)
                .IsRequired()
                .HasColumnName("PermissionId");

            builder.HasOne(x => x.User);
            builder.HasOne(x => x.Permission);
        }
    }
}
