using Xunit;
using Moq;
using UserRegistrationBackend.Models;
using UserRegistrationBackend.Repositories.Interfaces;
using UserRegistrationBackend.Validators;
using UserRegistrationBackend.DTOs.User;

namespace UserRegistrationBackend.Tests.Validators
{
    public class UserValidatorTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserValidator _userValidator;

        public UserValidatorTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userValidator = new UserValidator(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task ValidateEmail_EmailExists_ThrowsException()
        {
            var email = "existing@test.com";
            _userRepositoryMock.Setup(x => x.EmailExists(email))
                             .ReturnsAsync(true);

            var exception = await Assert.ThrowsAsync<Exception>(
                async () => await _userValidator.ValidateEmail(email)
            );
            Assert.Equal("Email already registered", exception.Message);
        }

        [Fact]
        public async Task ValidateEmail_EmailDoesNotExist_DoesNotThrowException()
        {
            var email = "new@test.com";
            _userRepositoryMock.Setup(x => x.EmailExists(email))
                             .ReturnsAsync(false);

            await _userValidator.ValidateEmail(email);
        }

        [Fact]
        public async Task ValidateUser_UserDoesNotExist_ThrowsException()
        {
            var userId = 1;
            _userRepositoryMock.Setup(x => x.GetById(userId))
                             .ReturnsAsync(() => null);
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _userValidator.ValidateUser(userId)
            );
            Assert.Equal($"User with ID {userId} not found", exception.Message);
        }

        [Theory]
        [InlineData("pass")]
        [InlineData("1234567")]
        public async Task ValidateCreateUser_PasswordIsTooShort_ThrowsException(string password)
        {
            var createUserDTO = new CreateUserDTO
            {
                Name = "Maria Silva",
                Email = "maria@test.com",
                Password = password,
                PhoneNumber = "1234567890"
            };

            var exception = await Assert.ThrowsAsync<Exception>(
                async () => await _userValidator.ValidateCreateUser(createUserDTO)
            );
            Assert.Equal("Password must be at least 8 characters long", exception.Message);
        }
    }
}
