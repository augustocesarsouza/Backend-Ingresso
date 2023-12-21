using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface ITheatreRepository
    {
        public Task<Theatre?> GetById(Guid guidId);
        public Task<Theatre?> GetByIdAllProps(Guid guid);
        public Task<List<Theatre>> GetAllTheatreByRegionId(Guid regionId);
        public Task<Theatre> Create(Theatre theatre);
        public Task<Theatre> Delete(Theatre movie);
        public Task<Theatre> Update(Theatre theatre);
    }
}
