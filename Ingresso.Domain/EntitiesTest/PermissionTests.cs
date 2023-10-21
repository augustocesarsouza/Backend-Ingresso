using Ingresso.Domain.Entities;
using Xunit;

namespace Ingresso.Domain.EntitiesTest
{
    public class PermissionTests
    {
        [Fact]
        public void Validator_Parameters()
        {
            // arrange
            var permission = new Permission();
            var visualName = "admin";
            var permissionName = "administrador";

            // act
            permission.Validator(visualName, permissionName);

            // assert
            Assert.Equal(visualName, permission.VisualName);
            Assert.Equal(permissionName, permission.PermissionName);
        }

        [Fact]
        public void Validator_Parameters_Only_VisualName_ThrowException()
        {
            // arrange
            var permission = new Permission();
            var visualName = "admin";
            var permissionName = "";

            // assert
            Assert.Throws<DomainValidationException>(() => permission.Validator(visualName, permissionName));
        }

        [Fact]
        public void Validator_Parameters_Only_PermissionName_ThrowException()
        {
            // arrange
            var permission = new Permission();
            var visualName = "";
            var permissionName = "administrator";

            // assert
            Assert.Throws<DomainValidationException>(() => permission.Validator(visualName, permissionName));
        }

        [Fact]
        public void Validator_Parameters_No_Paremeter_Provided_ThrowException()
        {
            // arrange
            var permission = new Permission();
            var visualName = "";
            var permissionName = "";

            // assert
            Assert.Throws<DomainValidationException>(() => permission.Validator(visualName, permissionName));
        }
    }
}
