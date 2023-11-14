using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using System.Text.RegularExpressions;

namespace Ingresso.Application.Services
{
    // Responsável por operações de autenticação, como login. Este serviço lida apenas com a autenticação e não possui lógica de envio de e-mails.
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGeneratorEmail _tokenGeneratorEmail;
        private readonly ITokenGeneratorCpf _tokenGeneratorCpf;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IPasswordHasherWrapper _passwordHasher;

        public UserAuthenticationService(
            IUserRepository userRepository, ITokenGeneratorEmail tokenGeneratorEmail, ITokenGeneratorCpf tokenGeneratorCpf,
            IMapper mapper, IUnitOfWork unitOfWork, IUserPermissionService userPermissionService, IPasswordHasherWrapper passwordHasher)
        {
            _userRepository = userRepository;
            _tokenGeneratorEmail = tokenGeneratorEmail;
            _tokenGeneratorCpf = tokenGeneratorCpf;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userPermissionService = userPermissionService;
            _passwordHasher = passwordHasher;
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

                if (!permission.IsSucess)
                    return ResultService.Fail<UserDto>("user does not have permission");

                var userPermissions = _mapper.Map<ICollection<UserPermission>>(permission.Data);

                var token = _tokenGeneratorEmail.Generator(user, userPermissions, password);

                if (!token.IsSucess)
                    return ResultService.Fail<UserDto>(token.Message ?? "error validate token");

                try
                {
                    await _unitOfWork.BeginTransaction();
                    user.ValidatorToken(token.Data.Acess_Token ?? "");
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

                var token = _tokenGeneratorCpf.Generator(user, userPermissions, password);

                if (!token.IsSucess)
                    return ResultService.Fail<UserDto>(token.Message ?? "error validate token");

                try
                {
                    await _unitOfWork.BeginTransaction();
                    user.ValidatorToken(token.Data.Acess_Token ?? ""); //Simular execão
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
