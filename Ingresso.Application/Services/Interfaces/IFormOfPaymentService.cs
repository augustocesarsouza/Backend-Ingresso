using Ingresso.Application.DTOs;

namespace Ingresso.Application.Services.Interfaces
{
    public interface IFormOfPaymentService
    {
        public Task<ResultService<List<FormOfPaymentDTO>>> GetMovieIDInfo(Guid movieId);
        public Task<ResultService<FormOfPaymentDTO>> Create(FormOfPaymentDTO? formOfPaymentDTO);
    }
}
