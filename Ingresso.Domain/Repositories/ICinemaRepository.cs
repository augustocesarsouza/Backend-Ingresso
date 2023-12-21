using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface ICinemaRepository
    {
        public Task<Cinema> Create(Cinema cinema);
    }
}
