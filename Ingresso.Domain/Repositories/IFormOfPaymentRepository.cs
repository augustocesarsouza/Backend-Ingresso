using Ingresso.Domain.Entities;

namespace Ingresso.Domain.Repositories
{
    public interface IFormOfPaymentRepository
    {
        public Task<List<FormOfPayment>> GetMovieIDInfo(Guid movieId);
        public Task<FormOfPayment> Create(FormOfPayment formOfPayment);
    }
}
