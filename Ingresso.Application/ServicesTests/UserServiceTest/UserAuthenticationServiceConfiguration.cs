using AutoMapper;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Repositories;
using Moq;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class UserAuthenticationServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IPasswordHasherWrapper> PasswordHasherGeneratorMock { get; }
        public Mock<ITokenGeneratorEmail> TokenGeneratorEmailMock { get; }
        public Mock<ITokenGeneratorCpf> TokenGeneratorCpfMock { get; }
        public Mock<IUserPermissionService> UserPermissionServiceMock { get; }

        public UserAuthenticationServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            PasswordHasherGeneratorMock = new();
            TokenGeneratorEmailMock = new();
            TokenGeneratorCpfMock = new();
            UserPermissionServiceMock = new();
        }
    }
}
