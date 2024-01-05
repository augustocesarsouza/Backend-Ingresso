using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ingresso.Infra.Data.Repositories
{
    public class FormOfPaymentRepository : IFormOfPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public FormOfPaymentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<FormOfPayment>> GetMovieIDInfo(Guid movieId)
        {
            var form = await _db.FormOfPayments
                .Where(x => x.MovieId == movieId)
                .Select(x => new FormOfPayment(x.FormName, x.Price))
                .ToListAsync();

            return form;
        }

        public async Task<FormOfPayment> Create(FormOfPayment formOfPayment)
        {
            await _db.FormOfPayments.AddAsync(formOfPayment);
            await _db.SaveChangesAsync();
            return formOfPayment;
        }
    }
}
