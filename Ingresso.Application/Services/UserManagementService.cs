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
                if (userDto.BirthDateString != null && userDto.BirthDateString?.Length > 0)
                {
                    var stringCortada = userDto.BirthDateString.Split('/');
                    var dia = stringCortada[0];
                    var mes = stringCortada[1];
                    var ano = stringCortada[2];

                    var birthDate = new DateTime(int.Parse(ano), int.Parse(mes), int.Parse(dia));

                    addInfoUser = new AdditionalInfoUserDTO(
                    birthDate, userDto.Gender, userDto.Phone, userDto.Cep, userDto.Logradouro, userDto.Numero,
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

                CreateInfoUser(addInfoUser);
                SendMessageEmail(data);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDto>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDto>($"{ex.Message}");
            }
        }

        public async void CreateInfoUser(AdditionalInfoUserDTO infoUserDTO)
        {
            await _additionalInfoUserService.CreateInfo(infoUserDTO);
        }

        public async void SendMessageEmail(User user)
        {
            await _sendEmailUser.SendEmail(user); // SE O EMAIL NÃO CHEGAR AO USUARIO DEIXAR ELE MANDAR DNV A CONFIRMAÇÃO
        }

        public async Task<ResultService<UserDto>> UpdateUser(UserDto userDto, string password)
        {
            if (userDto == null)
                return ResultService.Fail<UserDto>("Obj null");

            var guid = userDto.Id;

            if (guid == null)
                return ResultService.Fail<UserDto>("Id User it can not be null");

            var user = await _userRepository.GetUserById(guid.Value);

            if (user == null)
                return ResultService.Fail<UserDto>("user null database");

            if (!_passwordHasher.Verify(user.PasswordHash ?? "", password))
                return ResultService.Fail<UserDto>("password is not valid");

            user.ChangeNameUser(userDto.Name ?? "");

            try
            {
                await _unitOfWork.BeginTransaction();

                var userChange = await _userRepository.UpdateUser(user);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDto>($"{ex.Message}");
            }
        }

        public async Task<ResultService<UserDto>> UpdateUserPassword(UserPasswordChangeDTO userPasswordChangeDTO)
        {
            if(userPasswordChangeDTO == null)
                return ResultService.Fail<UserDto>("Obj null");

            var guid = userPasswordChangeDTO.IdGuid;

            if (guid == null)
                return ResultService.Fail<UserDto>("Id User it can not be null");

            var user = await _userRepository.GetUserById(guid.Value);

            if (user == null)
                return ResultService.Fail<UserDto>("user null database");

            if(userPasswordChangeDTO.PasswordCurrent == null || user.PasswordHash == null)
                return ResultService.Fail<UserDto>("some important information null");

            
            if(!_passwordHasher.Verify(user.PasswordHash, userPasswordChangeDTO.PasswordCurrent)) 
                return ResultService.Fail<UserDto>("password informed invalid");

            var newHashPassword = _passwordHasher.Hash(userPasswordChangeDTO.NewPassword ?? "");

            user.ChangePasswordHash(newHashPassword);

            try
            {
                await _unitOfWork.BeginTransaction();

                var userChange = await _userRepository.UpdateUser(user);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDto>(user));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDto>($"{ex.Message}");
            }
        }
    }
}
