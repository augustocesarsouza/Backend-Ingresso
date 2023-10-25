using AutoMapper;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Repositories;
using Moq;

namespace Ingresso.Application.ServicesTests
{
    public class UserServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<ITokenGeneratorEmail> TokenGeneratorEmailMock { get; }
        public Mock<ITokenGeneratorCpf> TokenGeneratorCpfMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<IUserPermissionService> UserPermissionServiceMock { get; }
        public Mock<IPasswordHasherWrapper> PasswordHasherGeneratorMock { get; }
        public Mock<IUserCreateDTOValidator> UserCreateDTOValidator { get; }

        public UserServiceConfiguration()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
            TokenGeneratorEmailMock = new Mock<ITokenGeneratorEmail>();
            TokenGeneratorCpfMock = new Mock<ITokenGeneratorCpf>();
            MapperMock = new Mock<IMapper>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UserPermissionServiceMock = new Mock<IUserPermissionService>();
            PasswordHasherGeneratorMock = new Mock<IPasswordHasherWrapper>();
            UserCreateDTOValidator = new Mock<IUserCreateDTOValidator>();
        }
    }
}
