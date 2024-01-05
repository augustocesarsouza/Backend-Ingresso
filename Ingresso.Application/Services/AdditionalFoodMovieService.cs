using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.UtilityExternal.Interface;

namespace Ingresso.Application.Services
{
    public class AdditionalFoodMovieService : IAdditionalFoodMovieService
    {
        private readonly IAdditionalFoodMovieRepository _additionalFoodMovieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly IAdditionalFoodMovieDTOValidator _validator;

        public AdditionalFoodMovieService(
            IAdditionalFoodMovieRepository additionalFoodMovieRepository, IUnitOfWork unitOfWork, IMapper mapper, 
            IAdditionalFoodMovieDTOValidator validator, ICloudinaryUti cloudinaryUti)
        {
            _additionalFoodMovieRepository = additionalFoodMovieRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<List<AdditionalFoodMovieDTO>>> GetAllFoodMovie(Guid movieId)
        {
            var foodsDTO = await _additionalFoodMovieRepository.GetAllFoodMovie(movieId);
            return ResultService.Ok(_mapper.Map<List<AdditionalFoodMovieDTO>>(foodsDTO));
        }

        public async Task<ResultService<AdditionalFoodMovieDTO>> Create(AdditionalFoodMovieDTO? additionalFoodMovieDTO)
        {
            if (additionalFoodMovieDTO == null)
                return ResultService.Fail<AdditionalFoodMovieDTO>("DTO cannot be null");

            var validate = _validator.ValidateDTO(additionalFoodMovieDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<AdditionalFoodMovieDTO>("Check the DTO infomed because it did not pass the validations", validate);

            var guidId = Guid.NewGuid();
            additionalFoodMovieDTO.Id = guidId;

            var result = await _cloudinaryUti.CreateImg(additionalFoodMovieDTO.Base64Img ?? "", 210, 210);

            if (result.ImgUrl == null || result.PublicId == null)
                return ResultService.Fail<AdditionalFoodMovieDTO>("error while creating while creating image");

            additionalFoodMovieDTO.ImgUrl = result.ImgUrl;
            additionalFoodMovieDTO.PublicId = result.PublicId;

            try
            {
                await _unitOfWork.BeginTransaction();

                var additionalFoodMovie = await _additionalFoodMovieRepository.Create(_mapper.Map<AdditionalFoodMovie>(additionalFoodMovieDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<AdditionalFoodMovieDTO>(additionalFoodMovie));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AdditionalFoodMovieDTO>($"There were any errors: ERROR: {ex.Message}");
            }
        }
    }
}
