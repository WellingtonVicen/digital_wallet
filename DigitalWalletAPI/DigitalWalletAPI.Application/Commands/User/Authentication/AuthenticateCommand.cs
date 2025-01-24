using DigitalWalletAPI.Application.DTOs.User.Authentication;
using MediatR;

namespace DigitalWalletAPI.Application.Commands.User.Authentication
{
    public class AuthenticateCommand : IRequest<AuthenticationResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
