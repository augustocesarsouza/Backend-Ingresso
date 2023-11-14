using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;

namespace Ingresso.Application.Services
{
    public class AdditionalInfoUserService : IAdditionalInfoUserService
    {
        private readonly IAdditionalInfoUserRepository _repositoryInfo;
        private readonly IAdditionalInfoUserDTOValidator _validatorDTO;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdditionalInfoUserService(IAdditionalInfoUserRepository repositoryInfo, IAdditionalInfoUserDTOValidator validatorDTO, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repositoryInfo = repositoryInfo;
            _validatorDTO = validatorDTO;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<AdditionalInfoUserDTO>> CreateInfo(AdditionalInfoUserDTO infoUserDTO)
        {
            if (infoUserDTO == null)
                return ResultService.Fail<AdditionalInfoUserDTO>("Error info user null");

            var result = _validatorDTO.ValidateDTO(infoUserDTO);
            if (!result.IsValid)
                return ResultService.RequestError<AdditionalInfoUserDTO>("error when validating the right check the information ", result);

            try
            {
                var infoUser = await _repositoryInfo.CreateInfo(_mapper.Map<AdditionalInfoUser>(infoUserDTO));

                if (infoUser == null)
                    return ResultService.Fail<AdditionalInfoUserDTO>("database returned null User");

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<AdditionalInfoUserDTO>(infoUserDTO));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<AdditionalInfoUserDTO>($"erro: {ex.Message}");
            }
        }
    }
}
