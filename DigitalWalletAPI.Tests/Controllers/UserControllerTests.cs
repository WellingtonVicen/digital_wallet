using DigitalWalletAPI.Application.Commands.User;
using DigitalWalletAPI.Application.DTOs.User;
using DigitalWalletAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DigitalWalletAPI.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new UserController(_mockMediator.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldPassCorrectCommandToMediator()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123"
            };

            CreateUserCommand sentCommand = null;
            var expectedResponse = new UserResponse
            {
                Id = 1,
                Name = request.Name,
                Email = request.Email
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((cmd, _) => sentCommand = cmd as CreateUserCommand)
                .ReturnsAsync(expectedResponse);

            // Act
            var response = await _controller.CreateUser(request);

            // Assert
            Assert.NotNull(sentCommand);
            Assert.Equal(request.Name, sentCommand.Name);
            Assert.Equal(request.Email, sentCommand.Email);
            Assert.Equal(request.Password, sentCommand.Password);

            var okResult = Assert.IsType<OkObjectResult>(response);
            var returnedValue = Assert.IsType<UserResponse>(okResult.Value);
            Assert.Equal(expectedResponse.Id, returnedValue.Id);
            Assert.Equal(expectedResponse.Name, returnedValue.Name);
            Assert.Equal(expectedResponse.Email, returnedValue.Email);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            var request = new CreateUserRequest
            {
                Name = null, // Invalid input
                Email = "testuser@example.com",
                Password = "securepassword123"
            };

            // Act
            var response = await _controller.CreateUser(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnInternalServerError_WhenResultIsNull()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "securepassword123"
            };

            // Simulando que o comando retorne null (falha no processo)
            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserResponse)null); // Simula uma falha retornando null

            // Act
            var response = await _controller.CreateUser(request);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(response); // Espera StatusCodeResult
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}
