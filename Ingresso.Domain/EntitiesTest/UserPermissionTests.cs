using Ingresso.Domain.Entities;
using System.Security;
using Xunit;

namespace Ingresso.Domain.EntitiesTest
{
    public class UserPermissionTests
    {
        [Fact]
        public void Validator_Parameters()
        {
            // arrange
            var userPermission = new UserPermission();
            var id = 1;
            var userId = Guid.NewGuid();
            var permission = new Permission();

            // act
            userPermission.Validator(id, userId, permission);

            // assert
            Assert.Equal(id, userPermission.Id);
            Assert.Equal(userId, userPermission.UserId);
            Assert.Equal(permission, userPermission.Permission);
        }

        [Fact]
        public void Validator_Parameters_Only_id_And_UserId_ThrowException()
        {
            // arrange
            var userPermission = new UserPermission();
            var id = 1;
            var userId = Guid.NewGuid();

            // assert
            Assert.Throws<DomainValidationException>(() => userPermission.Validator(id, userId, null));
        }

        [Fact]
        public void Validator_Parameters_Only_id_And_Permission_ThrowException()
        {
            // arrange
            var userPermission = new UserPermission();
            var id = 1;
            var userId = Guid.Empty;
            var permission = new Permission();

            // assert
            Assert.Throws<DomainValidationException>(() => userPermission.Validator(id, null, permission));
        }

        [Fact]
        public void Validator_Parameters_Only_UserId_And_Permission_ThrowException()
        {
            // arrange
            var userPermission = new UserPermission();
            var id = 0;
            var userId = Guid.NewGuid();
            var permission = new Permission();

            // assert
            Assert.Throws<DomainValidationException>(() => userPermission.Validator(id, userId, permission));
        }

        [Fact]
        public void Validator_Parameters_No_Paremeter_Provided_ThrowException()
        {
            // arrange
            var userPermission = new UserPermission();
            var id = 0;
            var userId = Guid.NewGuid();

            // assert
            Assert.Throws<DomainValidationException>(() => userPermission.Validator(id, userId, null));
        }
    }
}
