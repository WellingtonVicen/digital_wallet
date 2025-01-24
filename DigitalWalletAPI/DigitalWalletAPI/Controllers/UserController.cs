using DigitalWalletAPI.Application.Commands.User;
using DigitalWalletAPI.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="request">Dados do usuário para criação.</param>
        /// <returns>Usuário criado com sucesso.</returns>

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateUserCommand
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
