using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ingresso.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IPasswordHasherWrapper _passwordHasher;
        private readonly IUserCreateDTOValidator _userCreateDTOValidator;

        public UserService(
            IUserRepository userRepository, ITokenGenerator tokenGenerator,
            IMapper mapper, IUnitOfWork unitOfWork, IUserPermissionService userPermissionService,
            IPasswordHasherWrapper passwordHasher, IUserCreateDTOValidator userCreateDTOValidator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userPermissionService = userPermissionService;
            _passwordHasher = passwordHasher;
            _userCreateDTOValidator = userCreateDTOValidator;
        }

        public async Task<ResultService<UserDto>> CreateAsync(UserDto? userDto)
        {
            if (userDto == null)
                return ResultService.Fail<UserDto>("Obj null");

            var validation = _userCreateDTOValidator.ValidateUserDto(userDto);
            if (!validation.IsValid)
                return ResultService.RequestError<UserDto>("validation error check the information", validation);

            var userValidation = await _userRepository.CheckUserExits(userDto.Email ?? "", userDto.Cpf ?? "");

            if (userValidation != null)
                return ResultService.Fail<UserDto>("email or cpf already exist");

            try
            {
                await _unitOfWork.BeginTransaction();

                userDto.PasswordHash = _passwordHasher.Hash(userDto.Password ?? "");

                DateTime birthDate = DateTime.ParseExact(userDto.BirthDateString ?? "00/00/0000", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                userDto.BirthDate = birthDate;

                var data = await _userRepository.CreateAsync(_mapper.Map<User>(userDto));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDto>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDto>($"{ex.Message}");
            }
        }

        public async Task<ResultService<UserDto>> Login(string cpfOrEmail, string password)
        {
            if (cpfOrEmail.Contains('@'))
            {
                var user = await _userRepository.GetUserByEmail(cpfOrEmail);
                if (user == null)
                    return ResultService.Fail<UserDto>("User null");

                if (!_passwordHasher.Verify(user.PasswordHash ?? "", password))
                    return ResultService.Fail<UserDto>("password is not valid");

                var permission = await _userPermissionService.GetAllPermissionUser(user.Id);
                var userPermissions = _mapper.Map<ICollection<UserPermission>>(permission.Data);

                var token = _tokenGenerator.Generator(user, userPermissions);

                if (token == null)
                    return ResultService.Fail<UserDto>("error create token");

                try
                {
                    await _unitOfWork.BeginTransaction();
                    user.ValidatorToken(token.Acess_Token ?? "");
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<UserDto>(user));
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return ResultService.Fail<UserDto>(ex.Message);
                }
            }
            else if (Regex.IsMatch(cpfOrEmail, "^[0-9-]+$"))
            {
                var user = await _userRepository.GetUserByCpf(cpfOrEmail);
                if (user == null)
                    return ResultService.Fail<UserDto>("User null");

                if (!_passwordHasher.Verify(user.PasswordHash ?? "", password))
                    return ResultService.Fail<UserDto>("password is not valid");

                var permission = await _userPermissionService.GetAllPermissionUser(user.Id);

                if (!permission.IsSucess)
                    return ResultService.Fail<UserDto>("user does not have permission");

                var userPermissions = _mapper.Map<ICollection<UserPermission>>(permission.Data);

                var token = _tokenGenerator.Generator(user, userPermissions);

                if (token == null)
                    return ResultService.Fail<UserDto>("error create token");

                try
                {
                    await _unitOfWork.BeginTransaction();
                    user.ValidatorToken(token.Acess_Token ?? ""); //Simular execão
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<UserDto>(user));
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return ResultService.Fail<UserDto>(ex.Message);
                }
            }

            return ResultService.Fail<UserDto>("a valid email or valid CPF must be provided");
        }
    }
}
