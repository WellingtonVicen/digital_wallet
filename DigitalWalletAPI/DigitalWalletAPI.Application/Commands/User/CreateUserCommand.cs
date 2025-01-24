using DigitalWalletAPI.Application.DTOs.User;
using MediatR;

namespace DigitalWalletAPI.Application.Commands.User
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
