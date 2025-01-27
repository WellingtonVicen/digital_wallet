using MediatR;
using Moq;
using DigitalWalletAPI.Application.Commands.User.Authentication;
using DigitalWalletAPI.Application.DTOs.User.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DigitalWalletAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DigitalWalletAPI.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IMediator> _mockMediator;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMediator = new Mock<IMediator>();
            _controller = new AuthController(_mockConfiguration.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnOk_WhenUserIsAuthenticated()
        {
            // Arrange
            var request = new AuthenticateUserRequest
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var authenticateResult = new AuthenticationResponse
            {
                Data = new { Token = "fake-jwt-token" }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<AuthenticateCommand>(), default))
                .ReturnsAsync(authenticateResult);

            // Act
            var result = await _controller.Authenticate(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.Equal(authenticateResult, okResult.Value);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "The Email field is required.");

            var request = new AuthenticateUserRequest
            {
                Email = "",
                Password = "Password123"
            };

            // Act
            var result = await _controller.Authenticate(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUnauthorized_WhenAuthenticationFails()
        {
            // Arrange
            var request = new AuthenticateUserRequest
            {
                Email = "test@example.com",
                Password = "WrongPassword"
            };

            var authenticateResult = new AuthenticationResponse
            {
                Data = null
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<AuthenticateCommand>(), default))
                .ReturnsAsync(authenticateResult);

            // Act
            var result = await _controller.Authenticate(request);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Unauthorized, unauthorizedResult.StatusCode);
        }

        [Fact]
        public async Task Authenticate_ShouldSendCorrectCommandToMediator()
        {
            // Arrange
            var request = new AuthenticateUserRequest
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            AuthenticateCommand sentCommand = null;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<AuthenticateCommand>(), default))
                .Callback<AuthenticateCommand, CancellationToken>((cmd, _) => sentCommand = cmd)
                .ReturnsAsync(new AuthenticationResponse { Data = new { Token = "fake-jwt-token" } });

            // Act
            await _controller.Authenticate(request);

            // Assert
            Assert.NotNull(sentCommand);
            Assert.Equal(request.Email, sentCommand.Email);
            Assert.Equal(request.Password, sentCommand.Password);
        }
    }
}
