using DigitalWalletAPI.Application.Commands.User.Authentication;
using DigitalWalletAPI.Application.DTOs.User.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AuthController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("/api/v1/auth/login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AuthenticateCommand
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _mediator.Send(command);

            if (result.Data is not null)
            {
                return Ok(result);
            }

            return StatusCode((int)HttpStatusCode.Unauthorized);
        }

    }
}
