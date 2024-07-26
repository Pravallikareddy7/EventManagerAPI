using EventManagementApi.Controllers;
using EventManagementApi.Models;
using EventManagementApi.Repositories;
using EventManagementApi.Services;
using EventManagerAPI.Models.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EventManagementApi.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IConfiguration> _configMock;

        public UsersControllerTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(config => config["Jwt:Key"]).Returns("your_secret_key");
            _configMock.Setup(config => config["Jwt:Issuer"]).Returns("your_issuer");

            _controller = new UsersController(_userRepoMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenUserIsRegistered()
        {
            // Arrange
            var user = new User { Username = "testuser", PasswordHash = "testpassword" };
            _userRepoMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(user);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenUserRegistrationFails()
        {
            // Arrange
            var user = new User { Username = "testuser", PasswordHash = "testpassword" };
            _userRepoMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).ThrowsAsync(new Exception("Registration failed"));

            // Act
            var result = await _controller.Register(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            // Arrange
            _userRepoMock.Setup(repo => repo.GetUserByUsernameAsync("nonexistentuser")).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(new LoginRequest { Username = "nonexistentuser", Password = "password" });

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithToken_WhenUserFound()
        {
            // Arrange
            var user = new User { Username = "testuser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), Role = "User" };
            _userRepoMock.Setup(repo => repo.GetUserByUsernameAsync("testuser")).ReturnsAsync(user);

            // Act
            var result = await _controller.Login(new LoginRequest { Username = "testuser", Password = "password" });

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Logout_ReturnsOkResult()
        {
            // Act
            var result = await _controller.Logout();

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
