using AutoMapper;
using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.SendEmailUser;
using Moq;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class UserManagementServiceConfiguration
    {
        public Mock<IUserRepository> UserRepositoryMock { get; }
        public Mock<IMapper> MapperMock { get; }
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }
        public Mock<ISendEmailUser> SendEmailUser { get; }
        public Mock<IUserCreateDTOValidator> UserCreateDTOValidator { get; }
        public Mock<IPasswordHasherWrapper> PasswordHasherGeneratorMock { get; }
        public Mock<IAdditionalInfoUserService> AdditionalInfoUserService { get; }

        public UserManagementServiceConfiguration()
        {
            UserRepositoryMock = new();
            MapperMock = new();
            UnitOfWorkMock = new();
            SendEmailUser = new();
            UserCreateDTOValidator = new();
            PasswordHasherGeneratorMock = new();
            AdditionalInfoUserService = new();
        }
    }
}
