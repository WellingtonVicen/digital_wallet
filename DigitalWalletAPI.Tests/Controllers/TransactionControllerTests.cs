using DigitalWalletAPI.Application.Commands.Transaction;
using DigitalWalletAPI.Application.DTOs.Transaction;
using DigitalWalletAPI.Application.DTOs.Transaction.DigitalWalletAPI.API.DTOs.Transaction;
using DigitalWalletAPI.Application.Queries.Transaction;
using DigitalWalletAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DigitalWalletAPI.Tests.Controllers
{
    public class TransactionControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly TransactionController _controller;

        public TransactionControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new TransactionController(_mockMediator.Object);
        }

        [Fact]
        public async Task CreateTransaction_ShouldReturnCreatedAtAction_WhenTransactionIsCreated()
        {
            // Arrange
            var request = new CreateTransactionRequest
            {
                FromWalletId = 1,
                ToWalletId = 2,
                Amount = 100.50m,
                Description = "Test transaction"
            };

            var commandResult = new TransactionResponse
            {
                Id = 123,
                FromWalletId = 1,
                ToWalletId = 2,
                Amount = 100.50m,
                Description = "Test transaction",
                CreatedAt = DateTime.UtcNow
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(commandResult);

            // Act
            var result = await _controller.CreateTransaction(request, CancellationToken.None);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetTransactionsByUser), createdResult.ActionName);
            Assert.Equal(commandResult, createdResult.Value);
        }

        [Fact]
        public async Task CreateTransaction_ShouldPassCorrectCommandToMediator()
        {
            // Arrange
            var request = new CreateTransactionRequest
            {
                FromWalletId = 1,
                ToWalletId = 2,
                Amount = 150.75m,
                Description = "Another test"
            };

            CreateTransactionCommand sentCommand = null;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<IRequest<TransactionResponse>>(), It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((cmd, _) => sentCommand = cmd as CreateTransactionCommand)
                .ReturnsAsync(new TransactionResponse { Id = 456 });

            // Act
            await _controller.CreateTransaction(request, CancellationToken.None);

            // Assert
            Assert.NotNull(sentCommand);
            Assert.Equal(request.FromWalletId, sentCommand.FromWalletId);
            Assert.Equal(request.ToWalletId, sentCommand.ToWalletId);
            Assert.Equal(request.Amount, sentCommand.Amount);
            Assert.Equal(request.Description, sentCommand.Description);
        }

        [Fact]
        public async Task GetTransactionsByUser_ShouldReturnOkWithTransactions()
        {
            // Arrange
            long userId = 1;
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;

            var queryResult = new List<TransactionResponse>
        {
            new TransactionResponse { Id = 1, Amount = 50.00m, Description = "Test 1", CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new TransactionResponse { Id = 2, Amount = 150.00m, Description = "Test 2", CreatedAt = DateTime.UtcNow.AddDays(-1) }
        };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserTransactionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(queryResult);

            // Act
            var result = await _controller.GetTransactionsByUser(userId, startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(queryResult, okResult.Value);
        }

        [Fact]
        public async Task GetTransactionsByUser_ShouldPassCorrectQueryToMediator()
        {
            // Arrange
            long userId = 1;
            var startDate = DateTime.UtcNow.AddDays(-10);
            var endDate = DateTime.UtcNow;

            GetUserTransactionsQuery sentQuery = null;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<IRequest<List<TransactionResponse>>>(), It.IsAny<CancellationToken>()))
                .Callback<object, CancellationToken>((query, _) => sentQuery = query as GetUserTransactionsQuery)
                .ReturnsAsync(new List<TransactionResponse>());

            // Act
            await _controller.GetTransactionsByUser(userId, startDate, endDate);

            // Assert
            Assert.NotNull(sentQuery);
            Assert.Equal(userId, sentQuery.UserId);
            Assert.Equal(startDate, sentQuery.StartDate);
            Assert.Equal(endDate, sentQuery.EndDate);
        }
    }
}
