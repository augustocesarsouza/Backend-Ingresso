using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICinemaDTOValidator _cinemaDTOValidator;

        public CinemaService(ICinemaRepository cinemaRepository, IMapper mapper, IUnitOfWork unitOfWork, ICinemaDTOValidator cinemaDTOValidator)
        {
            _cinemaRepository = cinemaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cinemaDTOValidator = cinemaDTOValidator;
        }

        public async Task<ResultService<CinemaDTO>> Create(CinemaDTO? cinemaDTO)
        {
            if (cinemaDTO == null)
                return ResultService.Fail<CinemaDTO>("obj null");

            var validate = _cinemaDTOValidator.ValidateDTO(cinemaDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<CinemaDTO>("erro ao validar dto", validate);

            var guidId = Guid.NewGuid();
            cinemaDTO.Id = guidId;

            try
            {
                await _unitOfWork.BeginTransaction();

                var cinema = await _cinemaRepository.Create(_mapper.Map<Cinema>(cinemaDTO));

                if (cinema == null)
                    return ResultService.Fail<CinemaDTO>("error ao criar cinema");

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<CinemaDTO>(cinema));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CinemaDTO>($"error: {ex.Message}");
            }
        }
    }
}
