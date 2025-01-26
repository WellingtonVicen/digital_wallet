using DigitalWalletAPI.Application.Commands.Transaction;
using DigitalWalletAPI.Application.DTOs.Transaction.DigitalWalletAPI.API.DTOs.Transaction;
using DigitalWalletAPI.Application.Queries.Transaction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTransactionCommand
            {
                FromWalletId = request.FromWalletId,
                ToWalletId = request.ToWalletId,
                Amount = request.Amount,
                Description = request?.Description
            };

            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetTransactionsByUser), new { id = result.Id }, result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTransactionsByUser(long userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var query = new GetUserTransactionsQuery
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate
            };

            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }
    }
}
