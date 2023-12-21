using Ingresso.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Users = Set<User>();
            Permissions = Set<Permission>();
            UserPermissions = Set<UserPermission>();
            AdditionalInfoUsers = Set<AdditionalInfoUser>();
            Movies = Set<Movie>();
            MovieTheaters = Set<MovieTheater>();
            Theatres = Set<Theatre>();
            RegionTheatres = Set<RegionTheatre>();
            Regions = Set<Region>();
            Cinemas = Set<Cinema>();
            CinemaMovies = Set<CinemaMovie>();
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Permission> Permissions { get; private set; }
        public DbSet<UserPermission> UserPermissions { get; private set; }
        public DbSet<AdditionalInfoUser> AdditionalInfoUsers { get; private set; }
        public DbSet<Movie> Movies { get; private set; }
        public DbSet<MovieTheater> MovieTheaters { get; private set; }
        public DbSet<Theatre> Theatres { get; private set; }
        public DbSet<RegionTheatre> RegionTheatres { get; private set; }
        public DbSet<Region> Regions { get; private set; }
        public DbSet<Cinema> Cinemas { get; private set; }
        public DbSet<CinemaMovie> CinemaMovies { get; private set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
