using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.SendEmailUser;
using System.Globalization;

namespace Ingresso.Application.Services
{
    //Responsável por operações básicas de gerenciamento de usuários, como obter usuários e criar usuários.
    //Este serviço não deve conter lógica de autenticação ou envio de e-mails.
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendEmailUser _sendEmailUser;
        private readonly IUserCreateDTOValidator _userCreateDTOValidator;
        private readonly IPasswordHasherWrapper _passwordHasher;
        private readonly IAdditionalInfoUserService _additionalInfoUserService;

        public UserManagementService(
            IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, ISendEmailUser sendEmailUser,
            IUserCreateDTOValidator userCreateDTOValidator, IPasswordHasherWrapper passwordHasher, IAdditionalInfoUserService additionalInfoUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sendEmailUser = sendEmailUser;
            _userCreateDTOValidator = userCreateDTOValidator;
            _passwordHasher = passwordHasher;
            _additionalInfoUserService = additionalInfoUserService;
        }

        public async Task<ResultService<List<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            return ResultService.Ok(_mapper.Map<List<UserDto>>(users));
        }

        public async Task<ResultService<UserDto>> CheckEmailAlreadyExists(string email)
        {
            var user = await _userRepository.GetUserEmail(email);

            if (user == null)
                return ResultService.Fail<UserDto>("email não existe");

            return ResultService.Ok(_mapper.Map<UserDto>(user));
        }


        public async Task<ResultService<UserDto>> CreateAsync(UserDto? userDto)
        {
            if (userDto == null)
                return ResultService.Fail<UserDto>("Obj null");

            var validation = _userCreateDTOValidator.ValidateDTO(userDto);
            if (!validation.IsValid)
                return ResultService.RequestError<UserDto>("validation error check the information", validation);

            var userValidation = await _userRepository.CheckUserExits(userDto.Email ?? "", userDto.Cpf ?? "");

            if (userValidation != null)
                return ResultService.Fail<UserDto>("email or cpf already exist");

            Guid idUser = Guid.NewGuid();

            try
            {
                await _unitOfWork.BeginTransaction();

                userDto.PasswordHash = _passwordHasher.Hash(userDto.Password ?? "");
                userDto.Id = idUser;
                AdditionalInfoUserDTO addInfoUser = new();
                if (userDto.BirthDateString != null)
                {
                    var stringCortada = userDto.BirthDateString.Split('/');
                    var dia = stringCortada[0];
                    var mes = stringCortada[1];

                    var datanas = new DateTime(1000, int.Parse(mes), int.Parse(dia));

                    addInfoUser = new AdditionalInfoUserDTO(
                    datanas, userDto.Gender, userDto.Phone, userDto.Cep, userDto.Logradouro, userDto.Numero,
                    userDto.Complemento, userDto.Referencia, userDto.Bairro, userDto.Estado, userDto.Cidade, idUser);
                }
                else
                {
                    addInfoUser = new AdditionalInfoUserDTO(
                   null, userDto.Gender, userDto.Phone, userDto.Cep, userDto.Logradouro, userDto.Numero,
                   userDto.Complemento, userDto.Referencia, userDto.Bairro, userDto.Estado, userDto.Cidade, idUser);
                }

                userDto.ConfirmEmail = 0;
                var data = await _userRepository.CreateAsync(_mapper.Map<User>(userDto));

                if (data == null)
                    return ResultService.Fail<UserDto>("error ao criar user no banco");

                var dataInfoUser = await _additionalInfoUserService.CreateInfo(addInfoUser);// Colocar outra Thread

                if (!dataInfoUser.IsSucess)
                {
                    await _unitOfWork.Rollback();
                }

                var sendEmailSucess = await _sendEmailUser.SendEmail(data);

                if (!sendEmailSucess.IsSucess)
                    return ResultService.Fail<UserDto>("Erro no envio do email");

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDto>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDto>($"{ex.Message}");
            }
        }
    }
}
