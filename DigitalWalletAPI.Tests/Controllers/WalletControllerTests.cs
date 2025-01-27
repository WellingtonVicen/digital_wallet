using DigitalWalletAPI.Application.DTOs.Wallet;
using DigitalWalletAPI.Application.Queries.Wallet;
using DigitalWalletAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DigitalWalletAPI.Tests.Controllers
{
    public class WalletControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly WalletController _controller;

        public WalletControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new WalletController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetWalletBalance_ShouldReturnOkResult_WhenWalletExists()
        {
            // Arrange
            var walletBalanceResponse = new GetWalletBalanceResponse
            {
                Balance = 100.0m
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetWalletBalanceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(walletBalanceResponse);

            // Act
            var result = await _controller.GetWalletBalance(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBalance = Assert.IsType<GetWalletBalanceResponse>(okResult.Value);
            Assert.Equal(100.0m, returnedBalance.Balance);
        }

        [Fact]
        public async Task GetWalletBalance_ShouldReturnNotFound_WhenWalletDoesNotExist()
        {
            // Arrange
            long userId = 1;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetWalletBalanceQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetWalletBalanceResponse)null); // Simulando que não existe um saldo para o usuário

            // Act
            var result = await _controller.GetWalletBalance(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

}
