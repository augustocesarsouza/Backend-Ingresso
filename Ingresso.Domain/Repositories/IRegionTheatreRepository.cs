using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IRegionTheatreRepository
    {
        public Task<RegionTheatre> Create(RegionTheatre regionTheatre);
        public Task<List<RegionTheatre>?> GetByMovieId(Guid idTheatre);
        public Task<RegionTheatre> Delete(RegionTheatre movieTheater);
    }
}
