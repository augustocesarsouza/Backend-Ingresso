using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  : base(options)
        {
            Users = Set<User>();
            Permissions = Set<Permission>();
            UserPermissions = Set<UserPermission>();
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Permission> Permissions { get; private set; }
        public DbSet<UserPermission> UserPermissions { get; private set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
