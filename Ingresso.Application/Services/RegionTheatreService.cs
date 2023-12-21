using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class RegionTheatreService : IRegionTheatreService
    {
        private readonly IRegionTheatreRepository _regionTheatreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegionTheatreService(IRegionTheatreRepository regionTheatreRepository, IUnitOfWork unitOfWork)
        {
            _regionTheatreRepository = regionTheatreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<RegionTheatreDTO>> Delete(Guid idMovie)
        {
            var movieTheaterDelete = await _regionTheatreRepository.GetByMovieId(idMovie);

            if(movieTheaterDelete == null)
                return ResultService.Fail<RegionTheatreDTO>("retornou null Banco de dados");

            if (movieTheaterDelete.Count <= 0)
                return ResultService.Fail<RegionTheatreDTO>("não existe salas cinemas");

            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var itens in movieTheaterDelete)
                {
                    var deleteSala = await _regionTheatreRepository.Delete(itens);
                }

                await _unitOfWork.Commit();

                return ResultService.Ok<RegionTheatreDTO>("Tudo Certo foi deletado");
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<RegionTheatreDTO>("Algum erro na hora de deletar verifique");
            }
        }
    }
}
