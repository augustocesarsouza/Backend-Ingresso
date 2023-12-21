using FluentValidation.Results;
using Ingresso.Application.DTOs;
using Ingresso.Application.Services;
using Ingresso.Domain.Entities;
using Moq;
using System.ComponentModel;
using Xunit;

namespace Ingresso.Application.ServicesTests.UserServiceTest
{
    public class UserManagementServiceTest
    {
        private readonly UserManagementServiceConfiguration _configuration;
        private readonly UserManagementService _userManagementService;

        public UserManagementServiceTest()
        {
            _configuration = new UserManagementServiceConfiguration();
            var userManagementSer = new UserManagementService(
                _configuration.UserRepositoryMock.Object, _configuration.MapperMock.Object, _configuration.UnitOfWorkMock.Object,
                _configuration.SendEmailUser.Object, _configuration.UserCreateDTOValidator.Object, _configuration.PasswordHasherGeneratorMock.Object,
                _configuration.AdditionalInfoUserService.Object);

            _userManagementService = userManagementSer;
        }

        [Fact]
        public async void Should_Create_Account_Without_Errors()
        {
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
                Phone = "67 981523696",
                Gender = "Masculino",
                Cep = "79083590",
                Logradouro = "rua cajazeiras",
                Numero = "2420",
                Complemento = "prox escola maria lucia passarelli",
                Referencia = "escolha passarelli",
                Bairro = "Jardim aero rancho",
                Estado = "MS",
                Cidade = "Campo grande"
            };

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateDTO(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());

            _configuration.UserRepositoryMock.Setup(repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User?)null);
            _configuration.UserRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(new User());

            // Act
            var result = await _userManagementService.CreateAsync(userDto);

            // assert
            Assert.True(result.IsSucess, "criada a conta com sucesso");
        }

        [Fact]
        public async void Should_Create_Account_With_Error_DTO_Null()
        {
            var result = await _userManagementService.CreateAsync(null);

            Assert.False(result.IsSucess);
        }

        [Fact]
        [Description("When validating DTO returns 'IsValid' false")]
        public async void Should_Create_Account_With_Error_ValidateUserDto()
        {
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
            };

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateDTO(It.IsAny<UserDto>()))
            .Returns(new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("PropertyName", "Error message 1"),
            }));

            // Act
            var result = await _userManagementService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Account_With_Error_CheckUserExits()
        {
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
            };

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateDTO(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());
            _configuration.UserRepositoryMock.Setup
                (repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new User());

            // Act
            var result = await _userManagementService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        [Description("When trying to create a new user, the database returns null")]
        public async void Should_Throw_Error_By_Creating_User()
        {
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
            };

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateDTO(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());

            _configuration.UserRepositoryMock.Setup(repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User?)null);

            _configuration.UserRepositoryMock.Setup
                (repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync((User?)null);

            // Act
            var result = await _userManagementService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }

        [Fact]
        public async void Should_Create_Account_With_Error_ThrowException()
        {
            var userDto = new UserDto
            {
                Name = "Test",
                Email = "teste@gmail.com",
                BirthDateString = "05/10/1999",
                Cpf = "88888888888",
                Password = "Augusto92349923",
            };

            _configuration.UserCreateDTOValidator.Setup(vali => vali.ValidateDTO(It.IsAny<UserDto>()))
            .Returns(new ValidationResult());

            _configuration.UserRepositoryMock.Setup(repo => repo.CheckUserExits(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User?)null);

            _configuration.UserRepositoryMock.Setup
                (repo => repo.CreateAsync(It.IsAny<User>())).Throws(new Exception("Simulated database exception"));

            // Act
            var result = await _userManagementService.CreateAsync(userDto);

            // assert
            Assert.False(result.IsSucess);
        }
    }
}
