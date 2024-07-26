using EventManagementApi.Controllers;
using EventManagementApi.Models;
using EventManagementApi.Repositories;
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
        public async Task Register_ReturnsOkResult()
        {
            var user = new User { Username = "testuser", PasswordHash = "testpassword" };
            _userRepoMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            var result = await _controller.Register(user);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenUserNotFound()
        {
            _userRepoMock.Setup(repo => repo.GetUserByUsernameAsync("nonexistentuser")).ReturnsAsync((User)null);

            var result = await _controller.Login(new LoginRequest { Username = "nonexistentuser", Password = "password" });

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithToken()
        {
            var user = new User { Username = "testuser", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"), Role = "User" };
            _userRepoMock.Setup(repo => repo.GetUserByUsernameAsync("testuser")).ReturnsAsync(user);

            var result = await _controller.Login(new LoginRequest { Username = "testuser", Password = "password" });

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}
