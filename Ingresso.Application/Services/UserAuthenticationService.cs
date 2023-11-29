using AutoMapper;
using Ingresso.Application.CodeRandomUser;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Entities;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.SendEmailUser;
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
        private readonly ISendEmailUser _sendEmailUser;
        private readonly static CodeRandomDictionary _codeRandomDictionary = new();
        private static int RandomCode = 0;

        public UserAuthenticationService(
            IUserRepository userRepository, ITokenGeneratorEmail tokenGeneratorEmail, ITokenGeneratorCpf tokenGeneratorCpf,
            IMapper mapper, IUnitOfWork unitOfWork, IUserPermissionService userPermissionService, IPasswordHasherWrapper passwordHasher, ISendEmailUser sendEmailUser)
        {
            _userRepository = userRepository;
            _tokenGeneratorEmail = tokenGeneratorEmail;
            _tokenGeneratorCpf = tokenGeneratorCpf;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userPermissionService = userPermissionService;
            _passwordHasher = passwordHasher;
            _sendEmailUser = sendEmailUser;
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

                    var randomCode = GerarNumeroAleatorio();
                    _codeRandomDictionary.Add(user.Id.ToString(), randomCode);
                    //_sendEmailUser.SendCodeRandom(user, randomCode); Descomentar depois para enviar Email

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

        private static int GerarNumeroAleatorio()
        {
            Random random = new Random();
            return random.Next(100000, 1000000);
        }

        public ResultService<string> Verfic(int code, string guidId)
        {
            if (_codeRandomDictionary.Container(guidId, code))
            {
                _codeRandomDictionary.Remove(guidId);
                return ResultService.Ok<string>("ok");
            }
            else
            {
                return ResultService.Fail<string>("error");
            }
        }

        public ResultService<string> ResendCode(UserDto user)
        {
            var guidId = user.Id.ToString();
            if (guidId == null)
                return ResultService.Fail<string>("guid do usuario é null");

            if (_codeRandomDictionary.Container(guidId))
            {
                _codeRandomDictionary.Remove(guidId);
            }

            var randomCode = GerarNumeroAleatorio();
            _codeRandomDictionary.Add(guidId, randomCode);

            var result = _sendEmailUser.SendCodeRandom(_mapper.Map<User>(user), randomCode);
            if (result.IsSucess)
            {
                return ResultService.Ok<string>(result.Message ?? "tudo certo no envio");
            }
            else
            {
                return ResultService.Fail<string>(result.Message ?? "algum erro relacionado ao envio de email");
            }
        }
    }
}
