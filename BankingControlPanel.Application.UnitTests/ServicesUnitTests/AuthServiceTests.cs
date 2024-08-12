using BankingControlPanel.Application.Services;
using BankingControlPanel.Domain.Enums;
using BankingControlPanel.Domain.Models;
using BankingControlPanel.Interfaces.Services;
using BankingControlPanel.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BankingControlPanel.Application.UnitTests.ServicesUnitTests
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _mockUserManager = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);

            _mockSignInManager = new Mock<SignInManager<User>>(
                _mockUserManager.Object,
                new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                null, null, null, null);

            _authService = new AuthService(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnSuccess_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "Password123!",
                Role = Roles.User
            };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RegisterAsync(registerDto);

            // Assert
            Assert.True(result.Succeeded);
            _mockUserManager.Verify(x => x.CreateAsync(It.Is<User>(u => u.Email == registerDto.Email), registerDto.Password), Times.Once);
            _mockUserManager.Verify(x => x.AddToRoleAsync(It.Is<User>(u => u.Email == registerDto.Email), registerDto.Role.ToString()), Times.Once);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnFailure_WhenRegistrationFails()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                Email = "test@example.com",
                Password = "Password123!",
                Role = Roles.User
            };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to create user" }));

            // Act
            var result = await _authService.RegisterAsync(registerDto);

            // Assert
            Assert.False(result.Succeeded);
            _mockUserManager.Verify(x => x.CreateAsync(It.IsAny<User>(), registerDto.Password), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnSuccess_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _authService.LoginAsync(loginDto);

            // Assert
            Assert.Equal(SignInResult.Success, result);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnFailure_WhenLoginFails()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "WrongPassword!"
            };

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act
            var result = await _authService.LoginAsync(loginDto);

            // Assert
            Assert.Equal(SignInResult.Failed, result);
        }
    }
}
