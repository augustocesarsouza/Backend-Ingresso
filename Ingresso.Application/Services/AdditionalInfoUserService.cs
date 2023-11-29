using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Repositories;

namespace Ingresso.Application.Services
{
    public class AdditionalInfoUserService : IAdditionalInfoUserService
    {
        private readonly IAdditionalInfoUserRepository _repositoryInfo;
        private readonly IAdditionalInfoUserDTOValidator _validatorDTO;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasherWrapper _passwordHasher;
        private readonly IUserRepository _userRepository;

        public AdditionalInfoUserService(IAdditionalInfoUserRepository repositoryInfo, IAdditionalInfoUserDTOValidator validatorDTO, IMapper mapper, IUnitOfWork unitOfWork, IPasswordHasherWrapper passwordHasher, IUserRepository userRepository)
        {
            _repositoryInfo = repositoryInfo;
            _validatorDTO = validatorDTO;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<ResultService<AdditionalInfoUserDTO>> GetInfoUser(string idGuid)
        {
            var userInfo = await _repositoryInfo.GetInfoUser(Guid.Parse(idGuid));
            if (userInfo == null)
                return ResultService.Fail<AdditionalInfoUserDTO>("Erro obj info user null");

            return ResultService.Ok(_mapper.Map<AdditionalInfoUserDTO>(userInfo));
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

                //await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<AdditionalInfoUserDTO>(infoUserDTO));
            }
            catch (Exception ex)
            {
                //await _unitOfWork.Rollback();
                return ResultService.Fail<AdditionalInfoUserDTO>($"erro: {ex.Message}");
            }
        }

        public async Task<ResultService<AdditionalInfoUserDTO>> UpdateAsync(AdditionalInfoUserDTO infoUser, string password)
        {
            if (infoUser == null)
                return ResultService.Fail<AdditionalInfoUserDTO>("objInfoUser null");

            var user = await _userRepository.GetUserByEmailOnlyPasswordHash(infoUser.UserId);
            if (user == null)
                return ResultService.Fail<AdditionalInfoUserDTO>("User null");

            if (!_passwordHasher.Verify(user.PasswordHash ?? "", password))
                return ResultService.Fail<AdditionalInfoUserDTO>("password is not valid");

            var userAditionalInfo = await _repositoryInfo.GetByIdGuidUser(infoUser.UserId);

            if (userAditionalInfo == null)
                return ResultService.Fail<AdditionalInfoUserDTO>("erro ao pegar dados additionalInfoUser");

            if (infoUser.BirthDateString != null && infoUser.BirthDateString?.Length > 0)
            {
                var stringCortada = infoUser.BirthDateString.Split('/');
                var dia = stringCortada[0];
                var mes = stringCortada[1];
                var ano = stringCortada[2];

                var birthDate = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia));

                userAditionalInfo.AddData(birthDate, infoUser.Gender, infoUser.Phone, infoUser.Cep, infoUser.Logradouro, infoUser.Numero,
                infoUser.Complemento, infoUser.Referencia, infoUser.Bairro, infoUser.Estado, infoUser.Cidade, infoUser.UserId);
            }
            else
            {
                userAditionalInfo.AddData(null, infoUser.Gender, infoUser.Phone, infoUser.Cep, infoUser.Logradouro, infoUser.Numero,
                infoUser.Complemento, infoUser.Referencia, infoUser.Bairro, infoUser.Estado, infoUser.Cidade, infoUser.UserId);
            }

            var userInfo = await _repositoryInfo.UpdateAsync(_mapper.Map<AdditionalInfoUser>(userAditionalInfo));
            if (userInfo == null)
                return ResultService.Fail<AdditionalInfoUserDTO>("Erro ao update user");

            return ResultService.Ok(_mapper.Map<AdditionalInfoUserDTO>(userInfo));
        }
    }
}
