using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class CinemaMovieService : ICinemaMovieService
    {
        private readonly ICinemaMovieRepository _cinemaMovieRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICinemaMovieDTOValidator _cinemaMovieDTOValidator;
        private readonly IRegionService _regionService;

        public CinemaMovieService(ICinemaMovieRepository cinemaMovieRepository, IMapper mapper, IUnitOfWork unitOfWork, ICinemaMovieDTOValidator cinemaMovieDTOValidator, IRegionService regionService)
        {
            _cinemaMovieRepository = cinemaMovieRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cinemaMovieDTOValidator = cinemaMovieDTOValidator;
            _regionService = regionService;
        }

        public async Task<ResultService<List<CinemaMovieDTO>>> GetByRegionCinemaIdAndMovieId(string region, Guid movieId)
        {
            var regionId = await _regionService.GetRegionId(region);
            if (!regionId.IsSucess || regionId.Data == null)
                return ResultService.Fail<List<CinemaMovieDTO>>("error regionId not gfould");

            var result = await _cinemaMovieRepository.GetByRegionCinemaIdAndMovieId(regionId.Data.Id, movieId);
            //var retorno = ResultService.Ok(_mapper.Map<List<CinemaMovieDTO>>(result)); //Estou com erro se converto Map, de CinemaMovie para CinemaMovieDTO não pega o 'cinema'
            return ResultService.Ok(_mapper.Map<List<CinemaMovieDTO>>(result));
        }

        public async Task<ResultService<CinemaMovieDTO>> Create(CinemaMovieDTO? cinemaMovieDTO)
        {
            if (cinemaMovieDTO == null)
                return ResultService.Fail<CinemaMovieDTO>("obj null");

            var verific = _cinemaMovieDTOValidator.ValidateDTO(cinemaMovieDTO);
            if (!verific.IsValid)
                return ResultService.RequestError<CinemaMovieDTO>("error validar dto", verific);

            var idGuid = Guid.NewGuid();
            cinemaMovieDTO.Id = idGuid;

            try
            {
                await _unitOfWork.BeginTransaction();

                var cinemaMovie = await _cinemaMovieRepository.Create(_mapper.Map<CinemaMovie>(cinemaMovieDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<CinemaMovieDTO>(cinemaMovie));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CinemaMovieDTO>($"error: {ex.Message}");
            }
        }
    }
}
