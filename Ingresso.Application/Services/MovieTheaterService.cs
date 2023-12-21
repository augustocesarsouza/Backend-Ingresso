using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class MovieTheaterService : IMovieTheaterService
    {
        private readonly IMovieTheaterRepository _movieTheaterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieTheaterService(IMovieTheaterRepository movieTheaterRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _movieTheaterRepository = movieTheaterRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResultService<MovieTheaterDTO>> GetByIdMovie(Guid idMovie)
        {
            var movieTheater = await _movieTheaterRepository.GetByMovieId(idMovie);
            if (movieTheater == null)
                return ResultService.Fail<MovieTheaterDTO>("não encontrado sala");

            return ResultService.Ok(_mapper.Map<MovieTheaterDTO>(movieTheater));
        }

        public async Task<ResultService<MovieTheaterDTO>> Delete(Guid idMovie)
        {
            var movieTheaterDelete = await _movieTheaterRepository.GetByMovieId(idMovie);

            if(movieTheaterDelete == null)
                return ResultService.Fail<MovieTheaterDTO>("recebido null");

            if (movieTheaterDelete.Count <= 0)
                return ResultService.Fail<MovieTheaterDTO>("não existe salas cinemas");

            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var itens in movieTheaterDelete)
                {
                    var deleteSala = await _movieTheaterRepository.Delete(itens);
                }

                await _unitOfWork.Commit();

                return ResultService.Ok<MovieTheaterDTO>("Tudo Certo foi deletado");
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MovieTheaterDTO>("Algum erro na hora de deletar verifique");
            }
        }
    }
}
