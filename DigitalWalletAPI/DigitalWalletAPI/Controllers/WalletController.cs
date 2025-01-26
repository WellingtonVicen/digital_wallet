using DigitalWalletAPI.Application.Commands.Wallet;
using DigitalWalletAPI.Application.DTOs.Wallet;
using DigitalWalletAPI.Application.Queries.Wallet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consulta o saldo da carteira de um usuário.
        /// </summary>
        /// <param name="walletId">ID da carteira</param>
        /// <returns>Saldo da carteira</returns>

        [HttpGet("{userId}/balance")]
        public async Task<IActionResult> GetWalletBalance(long userId)
        {
            var query = new GetWalletBalanceQuery
            {
                UserId = userId
            };

            var result = await _mediator.Send(query);

            if (result is null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        [HttpPost("/add-balance")]
        public async Task<IActionResult> AddWalletBalanceAsync([FromBody] AddWalletBalanceRequest balanceRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddWalletBalanceCommand
            {
                UserId = balanceRequest.UserId,
                Amount = balanceRequest.Amount
            };

            var result =  await _mediator.Send(command);   

            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }
    }
}
