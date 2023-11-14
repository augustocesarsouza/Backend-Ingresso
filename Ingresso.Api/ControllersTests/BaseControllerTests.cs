using Ingresso.Api.Authentication;
using Ingresso.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Ingresso.Api.ControllersTests
{
    public class BaseControllerTests
    {
        [Fact]
        public void Should_Create_UserAuthDTO_Success_With_Email_And_Password()
        {
            var baseController = new BaseController();
            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetEmail("augusto@gmail.com");
            currentUser.SetPassword("Augusto92349923");
            currentUser.SetIsValid(true);

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.NotNull(userAuthDTO);
            Assert.Equal(userAuthDTO.Email, currentUser.Email);
            Assert.Equal(userAuthDTO.Password, currentUser.Password);
        }

        [Fact]
        public void Should_Create_UserAuthDTO_Success_With_Cpf_And_Password()
        {
            var baseController = new BaseController();
            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetCpf("88888888888");
            currentUser.SetPassword("Augusto92349923");
            currentUser.SetIsValid(true);

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.NotNull(userAuthDTO);
            Assert.Equal(userAuthDTO.Email, currentUser.Email);
            Assert.Equal(userAuthDTO.Password, currentUser.Password);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_Null()
        {
            var baseController = new BaseController();

            var userAuthDTO = baseController.Validator(null);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_Password_Null()
        {
            var baseController = new BaseController();

            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetPassword(null);

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_Password_Empty()
        {
            var baseController = new BaseController();

            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetPassword("");

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_Email_Null_And_Cpf_Null()
        {
            var baseController = new BaseController();

            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetEmail(null);
            currentUser.SetCpf(null);
            currentUser.SetPassword("Augusto92349923");

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_Email_Null_And_Cpf_Empty()
        {
            var baseController = new BaseController();

            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetEmail(null);
            currentUser.SetCpf("");
            currentUser.SetPassword("Augusto92349923");

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_Email_Empty_And_Cpf_Null()
        {
            var baseController = new BaseController();

            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetEmail("");
            currentUser.SetCpf(null);
            currentUser.SetPassword("Augusto92349923");

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Create_UserAuthDTO_Should_Return_Null_CurrentUser_IsValid_False()
        {
            var baseController = new BaseController();

            var currentUser = new CurrentUser(new HttpContextAccessor());
            currentUser.SetEmail("");
            currentUser.SetCpf(null);
            currentUser.SetPassword("Augusto92349923");
            currentUser.SetIsValid(false);

            var userAuthDTO = baseController.Validator(currentUser);

            Assert.Null(userAuthDTO);
        }

        [Fact]
        public void When_Call_Forbidden_Should_Return_403_Forbidden()
        {
            var baseController = new BaseController();

            var userAuthDTO = Assert.IsType<ObjectResult>(baseController.Forbidden());
            
            Assert.Equal(403, userAuthDTO.StatusCode);
        }
    }
}
